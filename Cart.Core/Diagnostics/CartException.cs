namespace Cart.Core.Diagnostics
{
    using System;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class CartException : Exception
    {
        public CartException(CartErrorCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public CartErrorCode ErrorCode { get; }
    }
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
}
