namespace Cart.Core
{
    using System;

#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class CartException : Exception
    {
        public CartException(CartItemErrorCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public CartItemErrorCode ErrorCode { get; }
    }
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
}
