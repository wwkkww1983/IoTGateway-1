﻿using System;
using System.Threading.Tasks;

namespace Waher.Persistence.Serialization.ValueTypes
{
	/// <summary>
	/// Serializes a <see cref="Char"/> value.
	/// </summary>
	public class CharSerializer : ValueTypeSerializer
	{
		/// <summary>
		/// Serializes a <see cref="Char"/> value.
		/// </summary>
		public CharSerializer()
		{
		}

		/// <summary>
		/// What type of object is being serialized.
		/// </summary>
		public override Type ValueType
		{
			get
			{
				return typeof(char);
			}
		}

		/// <summary>
		/// Deserializes an object from a binary source.
		/// </summary>
		/// <param name="Reader">Deserializer.</param>
		/// <param name="DataType">Optional datatype. If not provided, will be read from the binary source.</param>
		/// <param name="Embedded">If the object is embedded into another.</param>
		/// <returns>Deserialized object.</returns>
		public override Task<object> Deserialize(IDeserializer Reader, uint? DataType, bool Embedded)
		{
			if (!DataType.HasValue)
				DataType = Reader.ReadBits(6);

			switch (DataType.Value)
			{
				case ObjectSerializer.TYPE_CHAR: return Task.FromResult<object>(Reader.ReadChar());
				case ObjectSerializer.TYPE_BYTE: return Task.FromResult<object>((char)Reader.ReadByte());
				case ObjectSerializer.TYPE_INT16: return Task.FromResult<object>((char)Reader.ReadInt16());
				case ObjectSerializer.TYPE_INT32: return Task.FromResult<object>((char)Reader.ReadInt32());
				case ObjectSerializer.TYPE_INT64: return Task.FromResult<object>((char)Reader.ReadInt64());
				case ObjectSerializer.TYPE_SBYTE: return Task.FromResult<object>((char)Reader.ReadSByte());
				case ObjectSerializer.TYPE_UINT16: return Task.FromResult<object>((char)Reader.ReadUInt16());
				case ObjectSerializer.TYPE_UINT32: return Task.FromResult<object>((char)Reader.ReadUInt32());
				case ObjectSerializer.TYPE_UINT64: return Task.FromResult<object>((char)Reader.ReadUInt64());
				case ObjectSerializer.TYPE_DECIMAL: return Task.FromResult<object>((char)Reader.ReadDecimal());
				case ObjectSerializer.TYPE_DOUBLE: return Task.FromResult<object>((char)Reader.ReadDouble());
				case ObjectSerializer.TYPE_SINGLE: return Task.FromResult<object>((char)Reader.ReadSingle());
				case ObjectSerializer.TYPE_MIN: return Task.FromResult<object>(char.MinValue);
				case ObjectSerializer.TYPE_MAX: return Task.FromResult<object>(char.MaxValue);
				case ObjectSerializer.TYPE_NULL: return Task.FromResult<object>(null);

				case ObjectSerializer.TYPE_STRING:
				case ObjectSerializer.TYPE_CI_STRING:
					string s = Reader.ReadString();
					return Task.FromResult<object>(string.IsNullOrEmpty(s) ? (char)0 : s[0]);

				default: throw new Exception("Expected a char value.");
			}
		}

		/// <summary>
		/// Serializes an object to a binary destination.
		/// </summary>
		/// <param name="Writer">Serializer.</param>
		/// <param name="WriteTypeCode">If a type code is to be output.</param>
		/// <param name="Embedded">If the object is embedded into another.</param>
		/// <param name="Value">The actual object to serialize.</param>
		/// <param name="State">State object, passed on in recursive calls.</param>
		public override Task Serialize(ISerializer Writer, bool WriteTypeCode, bool Embedded, object Value, object State)
		{
			if (WriteTypeCode)
				Writer.WriteBits(ObjectSerializer.TYPE_CHAR, 6);

			Writer.Write((char)Value);

			return Task.CompletedTask;
		}

	}
}
