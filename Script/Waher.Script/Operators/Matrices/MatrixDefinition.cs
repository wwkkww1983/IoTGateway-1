﻿using System.Collections.Generic;
using Waher.Script.Abstraction.Elements;
using Waher.Script.Abstraction.Sets;
using Waher.Script.Exceptions;
using Waher.Script.Model;
using Waher.Script.Objects;
using Waher.Script.Objects.Matrices;
using Waher.Script.Objects.VectorSpaces;

namespace Waher.Script.Operators.Matrices
{
    /// <summary>
    /// Creates a matrix.
    /// </summary>
    public class MatrixDefinition : ElementList
    {
        /// <summary>
        /// Creates a matrix.
        /// </summary>
        /// <param name="Rows">Row vectors.</param>
        /// <param name="Start">Start position in script expression.</param>
        /// <param name="Length">Length of expression covered by node.</param>
		/// <param name="Expression">Expression containing script.</param>
        public MatrixDefinition(ScriptNode[] Rows, int Start, int Length, Expression Expression)
            : base(Rows, Start, Length, Expression)
        {
        }

        /// <summary>
        /// Evaluates the node, using the variables provided in the <paramref name="Variables"/> collection.
        /// </summary>
        /// <param name="Variables">Variables collection.</param>
        /// <returns>Result.</returns>
        public override IElement Evaluate(Variables Variables)
        {
            LinkedList<IElement> Rows = new LinkedList<IElement>();

            foreach (ScriptNode Node in this.Elements)
                Rows.AddLast(Node.Evaluate(Variables));

            return Encapsulate(Rows, this);
        }

        /// <summary>
        /// Encapsulates the elements of a matrix.
        /// </summary>
        /// <param name="Rows">Matrix rows.</param>
        /// <param name="Node">Script node from where the encapsulation is done.</param>
        /// <returns>Encapsulated matrix.</returns>
        public static IMatrix Encapsulate(ICollection<IElement> Rows, ScriptNode Node)
        {
            LinkedList<IElement> Elements = new LinkedList<IElement>();
            IVectorSpaceElement Vector;
            int? Columns = null;
            int i;

            foreach (IElement Row in Rows)
            {
                Vector = Row as IVectorSpaceElement;

                if (Vector is null)
                {
                    Columns = -1;
                    break;
                }
                else
                {
                    i = Vector.Dimension;
                    if (Columns.HasValue)
                    {
                        if (Columns.Value != i)
                        {
                            Columns = -1;
                            break;
                        }
                    }
                    else
                        Columns = i;

                    foreach (IElement Element in Vector.VectorElements)
                        Elements.AddLast(Element);
                }
            }

            if (!Columns.HasValue || Columns.Value < 0)
            {
                IVector V = Vectors.VectorDefinition.Encapsulate(Rows, false, Node);
                if (V is IMatrix M)
                    return M;
                else
                    throw new ScriptRuntimeException("Unable to convert vector of vectors to matrix.", Node);
            }
            else
                return Encapsulate(Elements, Rows.Count, Columns.Value, Node);
        }

        /// <summary>
        /// Encapsulates the elements of a matrix.
        /// </summary>
        /// <param name="Elements">Matrix elements.</param>
        /// <param name="Rows">Rows</param>
        /// <param name="Columns">Columns</param>
        /// <param name="Node">Script node from where the encapsulation is done.</param>
        /// <returns>Encapsulated matrix.</returns>
        public static IMatrix Encapsulate(ICollection<IElement> Elements, int Rows, int Columns, ScriptNode Node)
        {
            IElement SuperSetExample = null;
            IElement Element2;
            ISet CommonSuperSet = null;
            ISet Set;
            bool Upgraded = false;

			if (Elements.Count == Rows && Columns > 1)
			{
				List<IElement> Temp = new List<IElement>();

				foreach (IElement E in Elements)
				{
					if (E is IVector V)
						Temp.AddRange(V.VectorElements);
					else
						throw new ScriptRuntimeException("Invalid number of elements.", Node);
				}

				Elements = Temp;
			}

            foreach (IElement Element in Elements)
            {
                if (CommonSuperSet is null)
                {
                    SuperSetExample = Element;

                    if (Element is null)
                        CommonSuperSet = new ObjectValues();
                    else
                        CommonSuperSet = Element.AssociatedSet;
                }
                else
                {
                    if (Element is null)
                        Set = new ObjectValues();
                    else
                        Set = Element.AssociatedSet;

                    if (!Set.Equals(CommonSuperSet))
                    {
                        Element2 = Element;
                        if (!Expression.UpgradeField(ref Element2, ref Set, ref SuperSetExample, ref CommonSuperSet, Node))
                        {
                            CommonSuperSet = null;
                            break;
                        }
                        else
                            Upgraded = true;
                    }
                }
            }

            if (!(CommonSuperSet is null))
            {
                if (Upgraded)
                {
                    LinkedList<IElement> SuperElements = new LinkedList<IElement>();

                    foreach (IElement Element in Elements)
                    {
                        if (Element is null)
                            Set = new ObjectValues();
                        else
                            Set = Element.AssociatedSet;

                        if (Set.Equals(CommonSuperSet))
                            SuperElements.AddLast(Element);
                        else
                        {
                            Element2 = Element;
                            if (Expression.UpgradeField(ref Element2, ref Set, ref SuperSetExample, ref CommonSuperSet, Node) && Element2 is IVectorSpaceElement)
                                SuperElements.AddLast(Element2);
                            else
                            {
                                SuperElements = null;
                                CommonSuperSet = null;
                                break;
                            }
                        }
                    }

                    if (!(SuperElements is null))
                        Elements = SuperElements;
                }

                if (!(CommonSuperSet is null))
                {
                    if (CommonSuperSet is DoubleNumbers)
                        return new DoubleMatrix(Rows, Columns, Elements);
                    else if (CommonSuperSet is ComplexNumbers)
                        return new ComplexMatrix(Rows, Columns, Elements);
                    else if (CommonSuperSet is BooleanValues)
                        return new BooleanMatrix(Rows, Columns, Elements);
                }
            }

            return new ObjectMatrix(Rows, Columns, Elements);
        }

        /// <summary>
        /// Performs a pattern match operation.
        /// </summary>
        /// <param name="CheckAgainst">Value to check against.</param>
        /// <param name="AlreadyFound">Variables already identified.</param>
		/// <returns>Pattern match result</returns>
        public override PatternMatchResult PatternMatch(IElement CheckAgainst, Dictionary<string, IElement> AlreadyFound)
        {
            ScriptNode[] Elements = this.Elements;
			int c = Elements.Length;

			if (!(CheckAgainst is IMatrix Matrix) || Matrix.Rows != c)
				return PatternMatchResult.NoMatch;

			PatternMatchResult Result;
			int i;

			if (Matrix is IVector RowVectors)
			{
				i = 0;

				foreach (IElement E in RowVectors.VectorElements)
				{
					Result = Elements[i++].PatternMatch(E, AlreadyFound);
					if (Result != PatternMatchResult.Match)
						return Result;
				}
			}
			else
			{
				for (i = 0; i < c; i++)
				{
					Result = Elements[i].PatternMatch(Matrix.GetRow(i), AlreadyFound);
					if (Result != PatternMatchResult.Match)
						return Result;
				}
			}

			return PatternMatchResult.Match;
		}

    }
}
