﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Abstraction.Elements;
using Waher.Script.Exceptions;
using Waher.Script.Model;
using Waher.Script.Objects;

namespace Waher.Script.Operators.Membership
{
	/// <summary>
	/// Named method call operator.
	/// </summary>
	public class NamedMethodCall : NullCheckUnaryOperator
	{
		private readonly string name;
		private readonly ScriptNode[] parameters;
		private readonly int nrParameters;

		/// <summary>
		/// Named method call operator.
		/// </summary>
		/// <param name="Operand">Operand.</param>
		/// <param name="Name">Name</param>
		/// <param name="Parameters">Method arguments.</param>
		/// <param name="NullCheck">If null should be returned if operand is null.</param>
		/// <param name="Start">Start position in script expression.</param>
		/// <param name="Length">Length of expression covered by node.</param>
		/// <param name="Expression">Expression containing script.</param>
		public NamedMethodCall(ScriptNode Operand, string Name, ScriptNode[] Parameters, bool NullCheck, 
			int Start, int Length, Expression Expression)
			: base(Operand, NullCheck, Start, Length, Expression)
		{
			this.name = Name;
			this.parameters = Parameters;
			this.nrParameters = Parameters.Length;
		}

		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// Method arguments.
		/// </summary>
		public ScriptNode[] Parameters
		{
			get { return this.parameters; }
		}

		/// <summary>
		/// Evaluates the node, using the variables provided in the <paramref name="Variables"/> collection.
		/// </summary>
		/// <param name="Variables">Variables collection.</param>
		/// <returns>Result.</returns>
		public override IElement Evaluate(Variables Variables)
		{
			IElement E = this.op.Evaluate(Variables);
			return this.Evaluate(E, Variables);
		}

		private IElement Evaluate(IElement E, Variables Variables)
		{ 
			object Object = E.AssociatedObjectValue;
			Type T, PT;
			IElement[] Arguments = null;
			object[] ParameterValues;
			bool[] Extend;
			object Value;
			object Instance;
			int i;
			bool DoExtend = false;

			if (Object is null && this.nullCheck)
				return ObjectValue.Null;

			T = Object as Type;
			if (T is null)
			{
				T = Object?.GetType() ?? typeof(object);
				Instance = Object;
			}
			else
				Instance = null;

			lock (this.synchObject)
			{
				if (this.lastType != T)
				{
					this.method = null;
					this.methods = null;
					this.byReference = null;
					this.lastType = T;
				}

				Arguments = new IElement[this.nrParameters];
				for (i = 0; i < this.nrParameters; i++)
					Arguments[i] = this.parameters[i].Evaluate(Variables);

				if (!(this.method is null))
				{
					if (this.methodParametersTypes.Length != this.nrParameters)
					{
						this.method = null;
						this.methods = null;
						this.byReference = null;
					}
					else
					{
						for (i = 0; i < this.methodParametersTypes.Length; i++)
						{
							PT = this.methodParametersTypes[i].ParameterType;

							if (PT.IsByRef && Arguments[i].TryConvertTo(PT.GetElementType(), out Value))
							{
								this.methodArgumentExtensions[i] = false;
								this.methodArguments[i] = Value;
							}
							else if (Arguments[i].TryConvertTo(PT, out Value))
							{
								this.methodArgumentExtensions[i] = false;
								this.methodArguments[i] = Value;
							}
							else
							{
								if (Arguments[i].IsScalar)
									break;

								this.methodArgumentExtensions[i] = true;
								this.methodArguments[i] = null;
								DoExtend = true;
							}
						}

						if (i < this.methodParametersTypes.Length)
						{
							this.method = null;
							this.methods = null;
							this.byReference = null;
						}
					}
				}

				if (this.method is null)
				{
					if (this.methods is null)
						this.methods = this.GetMethods(T);

					List<KeyValuePair<string, int>> ByRef = null;
					ParameterValues = null;
					Extend = null;

					foreach (KeyValuePair<MethodInfo, ParameterInfo[]> P in this.methods)
					{
						DoExtend = false;

						if (Instance is null)
						{
							if (!P.Key.IsStatic)
								continue;
						}
						else
						{
							if (P.Key.IsStatic)
								continue;
						}

						for (i = 0; i < this.nrParameters; i++)
						{
							PT = P.Value[i].ParameterType;

							if (PT.IsByRef && Arguments[i].TryConvertTo(PT.GetElementType(), out Value))
							{
								if (ParameterValues is null)
								{
									Extend = new bool[this.nrParameters];
									ParameterValues = new object[this.nrParameters];
								}

								Extend[i] = false;
								ParameterValues[i] = Value;

								if (ByRef is null)
									ByRef = new List<KeyValuePair<string, int>>();

								if (this.parameters[i] is VariableReference Ref)
									ByRef.Add(new KeyValuePair<string, int>(Ref.VariableName, i));
								else
									ByRef.Add(new KeyValuePair<string, int>(null, i));
							}
							else if (Arguments[i].TryConvertTo(PT, out Value))
							{
								if (ParameterValues is null)
								{
									Extend = new bool[this.nrParameters];
									ParameterValues = new object[this.nrParameters];
								}

								Extend[i] = false;
								ParameterValues[i] = Value;
							}
							else
							{
								if (Arguments[i].IsScalar)
									break;

								if (Extend is null)
								{
									Extend = new bool[this.nrParameters];
									ParameterValues = new object[this.nrParameters];
								}

								Extend[i] = true;
								ParameterValues[i] = null;
								DoExtend = true;
							}
						}

						if (i < this.nrParameters)
						{
							ByRef?.Clear();
							continue;
						}

						this.method = P.Key;
						this.methodParametersTypes = P.Value;
						this.methodArguments = ParameterValues;
						this.methodArgumentExtensions = Extend;

						if (!(ByRef is null) && ByRef.Count > 0)
							this.byReference = ByRef.ToArray();
						else
							this.byReference = null;
						break;
					}

					if (this.method is null)
					{
						if (E.IsScalar)
							throw new ScriptRuntimeException("Invalid number or type of parameters.", this);

						LinkedList<IElement> Elements = new LinkedList<IElement>();

						foreach (IElement Item in E.ChildElements)
							Elements.AddLast(this.Evaluate(Item, Variables));

						return E.Encapsulate(Elements, this);
					}
				}
			}

			if (DoExtend)
			{
				if (!(this.byReference is null))
					throw new ScriptException("Canonical extensions of method calls having reference type arguments not supported.");	// TODO

				return this.EvaluateCanonical(Instance, this.method, this.methodParametersTypes, Arguments,
					this.methodArguments, this.methodArgumentExtensions);
			}
			else
			{
				Value = this.method.Invoke(Instance, this.methodArguments);

				if (!(this.byReference is null))
				{
					int j, c = this.byReference.Length;
					string s;

					for (i = 0; i < c; i++)
					{
						j = this.byReference[i].Value;
						if (string.IsNullOrEmpty(s = this.byReference[i].Key))
							Operators.PatternMatch.Match(this.parameters[j], Expression.Encapsulate(this.methodArguments[j]), Variables, this);
						else
							Variables[s] = this.methodArguments[j];
					}
				}

				return Expression.Encapsulate(Value);
			}
		}

		private IElement EvaluateCanonical(object Object, MethodInfo Method, ParameterInfo[] ParametersTypes,
			IElement[] Arguments, object[] ArgumentValues, bool[] Extend)
		{
			IEnumerator<IElement>[] Enumerators = null;
			ICollection<IElement> Children;
			IEnumerator<IElement> e;
			IElement First = null;
			int i, c = 0;

			for (i = 0; i < this.nrParameters; i++)
			{
				if (Extend[i])
				{
					if (Arguments[i].IsScalar)
					{
						if (!Arguments[i].TryConvertTo(ParametersTypes[i].ParameterType, out ArgumentValues[i]))
							throw new ScriptRuntimeException("Inconsistent argument types.", this);
					}
					else
					{
						Children = Arguments[i].ChildElements;
						if (First is null)
						{
							Enumerators = new IEnumerator<IElement>[this.nrParameters];
							First = Arguments[i];
							c = Children.Count;
						}
						else if (c != Children.Count)
							throw new ScriptRuntimeException("Incompatible dimensions.", this);

						Enumerators[i] = Children.GetEnumerator();
					}
				}
			}

			if (First is null)
				return Expression.Encapsulate(Method.Invoke(Object, ArgumentValues));

			LinkedList<IElement> Elements = new LinkedList<IElement>();
			Arguments = (IElement[])Arguments.Clone();

			while (true)
			{
				for (i = 0; i < this.nrParameters; i++)
				{
					if (!Extend[i])
						continue;

					if (!(e = Enumerators[i]).MoveNext())
						break;

					Arguments[i] = e.Current;
				}

				if (i < this.nrParameters)
					break;

				Elements.AddLast(this.EvaluateCanonical(Object, Method, ParametersTypes, Arguments, ArgumentValues, Extend));
			}

			for (i = 0; i < this.nrParameters; i++)
			{
				if (Extend[i])
					Enumerators[i].Dispose();
			}

			return First.Encapsulate(Elements, this);
		}

		private KeyValuePair<MethodInfo, ParameterInfo[]>[] GetMethods(Type Type)
		{
			List<KeyValuePair<MethodInfo, ParameterInfo[]>> Result = new List<KeyValuePair<MethodInfo, ParameterInfo[]>>();
			ParameterInfo[] ParameterInfo;
			IEnumerable<MethodInfo> Methods = Type.GetRuntimeMethods();

			foreach (MethodInfo MI in Methods)
			{
				if (MI.Name != this.name)
					continue;

				ParameterInfo = MI.GetParameters();
				if (ParameterInfo.Length != this.nrParameters)
					continue;

				Result.Add(new KeyValuePair<MethodInfo, ParameterInfo[]>(MI, ParameterInfo));
			}

			return Result.ToArray();
		}

		private Type lastType = null;
		private MethodInfo method = null;
		private ParameterInfo[] methodParametersTypes = null;
		private KeyValuePair<MethodInfo, ParameterInfo[]>[] methods = null;
		private KeyValuePair<string, int>[] byReference = null;
		private object[] methodArguments = null;
		private bool[] methodArgumentExtensions = null;
		private readonly object synchObject = new object();
	}
}
