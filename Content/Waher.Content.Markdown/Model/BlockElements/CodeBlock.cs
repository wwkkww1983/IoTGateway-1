﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Waher.Content.Xml;
using Waher.Events;
using Waher.Runtime.Inventory;

namespace Waher.Content.Markdown.Model.BlockElements
{
	/// <summary>
	/// Represents a code block in a markdown document.
	/// </summary>
	public class CodeBlock : BlockElement
	{
		private readonly ICodeContent handler;
		private readonly string[] rows;
		private readonly string indentString;
		private readonly string language;
		private readonly int start, end, indent;

		/// <summary>
		/// Represents a code block in a markdown document.
		/// </summary>
		/// <param name="Document">Markdown document.</param>
		/// <param name="Rows">Rows</param>
		/// <param name="Start">Start index of code.</param>
		/// <param name="End">End index of code.</param>
		/// <param name="Indent">Additional indenting.</param>
		public CodeBlock(MarkdownDocument Document, string[] Rows, int Start, int End, int Indent)
			: this(Document, Rows, Start, End, Indent, null)
		{
		}

		/// <summary>
		/// Represents a code block in a markdown document.
		/// </summary>
		/// <param name="Document">Markdown document.</param>
		/// <param name="Rows">Rows</param>
		/// <param name="Start">Start index of code.</param>
		/// <param name="End">End index of code.</param>
		/// <param name="Indent">Additional indenting.</param>
		/// <param name="Language">Language used.</param>
		public CodeBlock(MarkdownDocument Document, string[] Rows, int Start, int End, int Indent, string Language)
			: base(Document)
		{
			this.rows = Rows;
			this.start = Start;
			this.end = End;
			this.indent = Indent;
			this.indentString = this.indent <= 0 ? string.Empty : new string('\t', this.indent);
			this.language = Language;
			this.handler = GetCodeBlockHandler(this.language);
			this.handler?.Register(Document);
		}

		/// <summary>
		/// Language
		/// </summary>
		public string Language => this.language;

		/// <summary>
		/// Rows in code block
		/// </summary>
		public string[] Rows => this.rows;

		/// <summary>
		/// Code block handler.
		/// </summary>
		public ICodeContent Handler => this.handler;

		private static ICodeContent[] codeContents = null;
		private static IXmlVisualizer[] xmlVisualizers = null;
		private readonly static Dictionary<string, ICodeContent[]> codeContentHandlers = new Dictionary<string, ICodeContent[]>(StringComparer.CurrentCultureIgnoreCase);
		private readonly static Dictionary<string, IXmlVisualizer[]> xmlVisualizerHandlers = new Dictionary<string, IXmlVisualizer[]>(StringComparer.CurrentCultureIgnoreCase);

		static CodeBlock()
		{
			Init();
			Types.OnInvalidated += (sender, e) => Init();
		}

		private static void Init()
		{
			List<ICodeContent> CodeContents = new List<ICodeContent>();
			List<IXmlVisualizer> XmlVisualizers = new List<IXmlVisualizer>();
			TypeInfo TI;

			foreach (Type T in Types.GetTypesImplementingInterface(typeof(ICodeContent)))
			{
				TI = T.GetTypeInfo();
				if (TI.IsAbstract || TI.IsGenericTypeDefinition)
					continue;

				try
				{
					ICodeContent CodeContent = (ICodeContent)Activator.CreateInstance(T);
					CodeContents.Add(CodeContent);
				}
				catch (Exception ex)
				{
					Log.Critical(ex);
				}
			}

			foreach (Type T in Types.GetTypesImplementingInterface(typeof(IXmlVisualizer)))
			{
				TI = T.GetTypeInfo();
				if (TI.IsAbstract || TI.IsGenericTypeDefinition)
					continue;

				try
				{
					IXmlVisualizer XmlVisualizer = (IXmlVisualizer)Activator.CreateInstance(T);
					XmlVisualizers.Add(XmlVisualizer);
				}
				catch (Exception ex)
				{
					Log.Critical(ex);
				}
			}

			lock (codeContentHandlers)
			{
				codeContents = CodeContents.ToArray();
				codeContentHandlers.Clear();
			}

			lock (xmlVisualizerHandlers)
			{
				xmlVisualizers = XmlVisualizers.ToArray();
				xmlVisualizerHandlers.Clear();
			}
		}

		internal static ICodeContent GetCodeBlockHandler(string Language)
		{
			ICodeContent[] Handlers;

			if (string.IsNullOrEmpty(Language))
				return null;

			lock (codeContentHandlers)
			{
				if (!codeContentHandlers.TryGetValue(Language, out Handlers))
				{
					List<ICodeContent> List = new List<ICodeContent>();

					foreach (ICodeContent Content in codeContents)
					{
						if (Content.Supports(Language) > Grade.NotAtAll)
							List.Add(Content);
					}

					if (List.Count > 0)
						Handlers = List.ToArray();
					else
						Handlers = null;

					codeContentHandlers[Language] = Handlers;
				}
			}

			if (Handlers is null)
				return null;

			ICodeContent Best = null;
			Grade BestGrade = Grade.NotAtAll;
			Grade ContentGrade;

			foreach (ICodeContent Content in Handlers)
			{
				ContentGrade = Content.Supports(Language);
				if (ContentGrade > BestGrade)
				{
					BestGrade = ContentGrade;
					Best = Content;
				}
			}

			return Best;
		}

		internal static IXmlVisualizer GetXmlVisualizerHandler(XmlDocument Xml)
		{
			if (Xml is null || Xml.DocumentElement is null)
				return null;

			IXmlVisualizer[] Handlers;
			string Key = Xml.DocumentElement.NamespaceURI + "#" + Xml.DocumentElement.LocalName;

			lock (xmlVisualizerHandlers)
			{
				if (!xmlVisualizerHandlers.TryGetValue(Key, out Handlers))
				{
					List<IXmlVisualizer> List = new List<IXmlVisualizer>();

					foreach (IXmlVisualizer Visualizer in xmlVisualizers)
					{
						if (Visualizer.Supports(Xml) > Grade.NotAtAll)
							List.Add(Visualizer);
					}

					if (List.Count > 0)
						Handlers = List.ToArray();
					else
						Handlers = null;

					xmlVisualizerHandlers[Key] = Handlers;
				}
			}

			if (Handlers is null)
				return null;

			IXmlVisualizer Best = null;
			Grade BestGrade = Grade.NotAtAll;
			Grade VisualizerGrade;

			foreach (IXmlVisualizer Visualizer in Handlers)
			{
				VisualizerGrade = Visualizer.Supports(Xml);
				if (VisualizerGrade > BestGrade)
				{
					BestGrade = VisualizerGrade;
					Best = Visualizer;
				}
			}

			return Best;
		}

		/// <summary>
		/// Generates Markdown for the markdown element.
		/// </summary>
		/// <param name="Output">Markdown will be output here.</param>
		public override void GenerateMarkdown(StringBuilder Output)
		{
			Output.Append("```");
			Output.AppendLine(this.language);

			foreach (string Row in this.rows)
				Output.AppendLine(Row);

			Output.AppendLine("```");
			Output.AppendLine();
		}

		/// <summary>
		/// Generates HTML for the markdown element.
		/// </summary>
		/// <param name="Output">HTML will be output here.</param>
		public override void GenerateHTML(StringBuilder Output)
		{
			if (this.handler != null && this.handler.HandlesHTML)
			{
				try
				{
					if (this.handler.GenerateHTML(Output, this.rows, this.language, this.indent, this.Document))
						return;
				}
				catch (Exception ex)
				{
					ex = Log.UnnestException(ex);

					if (ex is AggregateException ex2)
					{
						foreach (Exception ex3 in ex2.InnerExceptions)
						{
							Output.Append("<p><font class=\"error\">");
							Output.Append(XML.HtmlValueEncode(ex3.Message));
							Output.AppendLine("</font></p>");
						}
					}
					else
					{
						Output.Append("<p><font class=\"error\">");
						Output.Append(XML.HtmlValueEncode(ex.Message));
						Output.Append("</font></p>");
					}
				}
			}

			int i;

			Output.Append("<pre><code class=\"");

			if (string.IsNullOrEmpty(this.language))
				Output.Append("nohighlight");
			else
				Output.Append(XML.Encode(this.language));

			Output.Append("\">");

			for (i = this.start; i <= this.end; i++)
			{
				Output.Append(this.indentString);
				Output.AppendLine(XML.HtmlValueEncode(this.rows[i]));
			}

			Output.AppendLine("</code></pre>");
		}

		/// <summary>
		/// Generates plain text for the markdown element.
		/// </summary>
		/// <param name="Output">Plain text will be output here.</param>
		public override void GeneratePlainText(StringBuilder Output)
		{
			if (this.handler != null && this.handler.HandlesPlainText)
			{
				try
				{
					if (this.handler.GeneratePlainText(Output, this.rows, this.language, this.indent, this.Document))
						return;
				}
				catch (Exception ex)
				{
					ex = Log.UnnestException(ex);

					if (ex is AggregateException ex2)
					{
						foreach (Exception ex3 in ex2.InnerExceptions)
							Output.AppendLine(ex3.Message);
					}
					else
						Output.AppendLine(ex.Message);
				}
			}

			int i;

			for (i = this.start; i <= this.end; i++)
			{
				Output.Append(this.indentString);
				Output.AppendLine(this.rows[i]);
			}

			Output.AppendLine();
		}

		/// <summary>
		/// Generates WPF XAML for the markdown element.
		/// </summary>
		/// <param name="Output">XAML will be output here.</param>
		/// <param name="TextAlignment">Alignment of text in element.</param>
		public override void GenerateXAML(XmlWriter Output, TextAlignment TextAlignment)
		{
			XamlSettings Settings = this.Document.Settings.XamlSettings;

			if (this.handler != null && this.handler.HandlesXAML)
			{
				try
				{
					if (this.handler.GenerateXAML(Output, TextAlignment, this.rows, this.language, this.indent, this.Document))
						return;
				}
				catch (Exception ex)
				{
					ex = Log.UnnestException(ex);

					if (ex is AggregateException ex2)
					{
						foreach (Exception ex3 in ex2.InnerExceptions)
						{
							Output.WriteStartElement("TextBlock");
							Output.WriteAttributeString("TextWrapping", "Wrap");
							Output.WriteAttributeString("Margin", Settings.ParagraphMargins);

							if (TextAlignment != TextAlignment.Left)
								Output.WriteAttributeString("TextAlignment", TextAlignment.ToString());

							Output.WriteAttributeString("Foreground", "Red");
							Output.WriteValue(ex3.Message);
							Output.WriteEndElement();
						}
					}
					else
					{
						Output.WriteStartElement("TextBlock");
						Output.WriteAttributeString("TextWrapping", "Wrap");
						Output.WriteAttributeString("Margin", Settings.ParagraphMargins);
						if (TextAlignment != TextAlignment.Left)
							Output.WriteAttributeString("TextAlignment", TextAlignment.ToString());

						Output.WriteAttributeString("Foreground", "Red");
						Output.WriteValue(ex.Message);
						Output.WriteEndElement();
					}
				}
			}

			bool First = true;

			Output.WriteStartElement("TextBlock");
			Output.WriteAttributeString("xml", "space", null, "preserve");
			Output.WriteAttributeString("TextWrapping", "NoWrap");
			Output.WriteAttributeString("Margin", Settings.ParagraphMargins);
			Output.WriteAttributeString("FontFamily", "Courier New");
			if (TextAlignment != TextAlignment.Left)
				Output.WriteAttributeString("TextAlignment", TextAlignment.ToString());

			foreach (string Row in this.rows)
			{
				if (First)
					First = false;
				else
					Output.WriteElementString("LineBreak", string.Empty);

				Output.WriteValue(Row);
			}

			Output.WriteEndElement();
		}

		/// <summary>
		/// Generates Xamarin.Forms XAML for the markdown element.
		/// </summary>
		/// <param name="Output">XAML will be output here.</param>
		/// <param name="TextAlignment">Alignment of text in element.</param>
		public override void GenerateXamarinForms(XmlWriter Output, TextAlignment TextAlignment)
		{
			XamlSettings Settings = this.Document.Settings.XamlSettings;

			if (this.handler != null && this.handler.HandlesXAML)
			{
				try
				{
					if (this.handler.GenerateXamarinForms(Output, TextAlignment, this.rows, this.language, this.indent, this.Document))
						return;
				}
				catch (Exception ex)
				{
					ex = Log.UnnestException(ex);

					if (ex is AggregateException ex2)
					{
						foreach (Exception ex3 in ex2.InnerExceptions)
						{
							Paragraph.GenerateXamarinFormsContentView(Output, TextAlignment, Settings);
							Output.WriteStartElement("Label");
							Output.WriteAttributeString("LineBreakMode", "WordWrap");
							Output.WriteAttributeString("TextColor", "Red");
							Output.WriteValue(ex3.Message);
							Output.WriteEndElement();
							Output.WriteEndElement();
						}
					}
					else
					{
						Paragraph.GenerateXamarinFormsContentView(Output, TextAlignment, Settings);
						Output.WriteStartElement("Label");
						Output.WriteAttributeString("LineBreakMode", "WordWrap");
						Output.WriteAttributeString("TextColor", "Red");
						Output.WriteValue(ex.Message);
						Output.WriteEndElement();
						Output.WriteEndElement();
					}
				}
			}

			Paragraph.GenerateXamarinFormsContentView(Output, TextAlignment, Settings);
			Output.WriteStartElement("StackLayout");
			Output.WriteAttributeString("Orientation", "Vertical");

			foreach (string Row in this.rows)
			{
				Output.WriteStartElement("Label");
				Output.WriteAttributeString("LineBreakMode", "NoWrap");
				Output.WriteAttributeString("FontFamily", "Courier New");
				Output.WriteAttributeString("Text", Row);
				Output.WriteEndElement();
			}

			Output.WriteEndElement();
			Output.WriteEndElement();
		}

		/// <summary>
		/// Code block indentation.
		/// </summary>
		public int Indent
		{
			get { return this.indent; }
		}

		/// <summary>
		/// If the element is an inline span element.
		/// </summary>
		internal override bool InlineSpanElement
		{
			get { return false; }
		}

		/// <summary>
		/// Exports the element to XML.
		/// </summary>
		/// <param name="Output">XML Output.</param>
		public override void Export(XmlWriter Output)
		{
			Output.WriteStartElement("CodeBlock");
			Output.WriteAttributeString("language", this.language);
			Output.WriteAttributeString("start", this.start.ToString());
			Output.WriteAttributeString("end", this.end.ToString());
			Output.WriteAttributeString("indent", this.indent.ToString());
			Output.WriteAttributeString("indentString", this.indentString);

			foreach (string s in this.rows)
				Output.WriteElementString("Row", s);

			Output.WriteEndElement();
		}

		/// <summary>
		/// If the current object has same meta-data as <paramref name="E"/>
		/// (but not necessarily same content).
		/// </summary>
		/// <param name="E">Element to compare to.</param>
		/// <returns>If same meta-data as <paramref name="E"/>.</returns>
		public override bool SameMetaData(MarkdownElement E)
		{
			return E is CodeBlock x &&
				this.indent == x.indent &&
				this.indentString == x.indentString &&
				this.language == x.language &&
				AreEqual(this.rows, x.rows) &&
				base.SameMetaData(E);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return obj is CodeBlock x &&
				this.indent == x.indent &&
				this.indentString == x.indentString &&
				this.language == x.language &&
				AreEqual(this.rows, x.rows) &&
				base.Equals(obj);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int h1 = base.GetHashCode();
			int h2 = this.indent.GetHashCode();

			h1 = ((h1 << 5) + h1) ^ h2;
			h2 = this.indentString?.GetHashCode() ?? 0;

			h1 = ((h1 << 5) + h1) ^ h2;
			h2 = this.language?.GetHashCode() ?? 0;

			h1 = ((h1 << 5) + h1) ^ h2;
			h2 = GetHashCode(this.rows);

			h1 = ((h1 << 5) + h1) ^ h2;

			return h1;
		}

	}
}
