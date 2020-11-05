using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Botix.Common.Logging.Context
{
    /// <summary>
    ///     Call context state properties.
    /// </summary>
    public sealed class ContextProperties
    {
        /// <summary>
        ///     Correlation ID key. Used as header and log's property name.
        /// </summary>
        public const string CorrelationIdKey = "x-correlation-id";

        /// <summary>
        ///     Request ID key. Used as header and log's property name.
        /// </summary>
        public const string RequestIdKey = "x-request-id";

        /// <summary>
        ///     User ID key. Used as header and log's property name.
        /// </summary>
        public const string UserIdKey = "x-user-id";

        /// <summary>
        ///     Correlation ID used to identify process inside application.
        /// </summary>
        public string CorrelationId { get; private set; }

        /// <summary>
        ///     Request ID used to identify process between applications.
        /// </summary>
        public string RequestId { get; private set; }

        /// <summary>
        ///     Usert ID used to identify processes initiated by single user between applications.
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        ///     Creates context properties from request headers.
        /// </summary>
        /// <param name="headers">The request and response headers.</param>
        public static ContextProperties FromHeaders(IDictionary<string, StringValues> headers)
        {
            headers.TryGetValue(RequestIdKey, out var requestId);
            if (StringValues.IsNullOrEmpty(requestId))
                requestId = Guid.NewGuid().ToString();

            headers.TryGetValue(UserIdKey, out var userId);

            var correlationId = Guid.NewGuid().ToString();

            return Construct(b =>
            {
                b.RequestId(requestId);
                b.CorrelationId(correlationId);
                b.UserId(userId);
            });
        }

        /// <summary>
        ///     Creates call context state properties from build action.
        /// </summary>
        /// <param name="build">Call context state properties build action.</param>
        public static ContextProperties Construct(Action<ContextPropertiesBuilder> build)
        {
            return ContextPropertiesBuilder.Build(build);
        }

        /// <summary>
        ///     Call context state properties builder.
        /// </summary>
        public class ContextPropertiesBuilder
        {
            private readonly ContextProperties _properties = new ContextProperties();

            private ContextPropertiesBuilder() { }

            /// <summary>
            ///     Creates call context state properties from build action.
            /// </summary>
            /// <param name="build">Call context state properties build action.</param>
            public static ContextProperties Build(Action<ContextPropertiesBuilder> build)
            {
                var builder = new ContextPropertiesBuilder();
                build(builder);

                return builder.ToProperties();
            }

            /// <summary>
            ///     Set call context state correlation ID.
            /// </summary>
            /// <param name="correlationId">Correlation ID value.</param>
            public ContextPropertiesBuilder CorrelationId(string correlationId)
            {
                _properties.CorrelationId = correlationId;
                return this;
            }

            /// <summary>
            ///     Set call context state request ID.
            /// </summary>
            /// <param name="requestId">Request ID value.</param>
            public ContextPropertiesBuilder RequestId(string requestId)
            {
                _properties.RequestId = requestId;
                return this;
            }

            /// <summary>
            ///     Set call context state user ID.
            /// </summary>
            /// <param name="userId">User ID value.</param>
            public ContextPropertiesBuilder UserId(string userId)
            {
                _properties.UserId = userId;
                return this;
            }

            /// <summary>
            ///     Launches call context state properties build process.
            /// </summary>
            public ContextProperties ToProperties() => _properties;
        }
    }
}

