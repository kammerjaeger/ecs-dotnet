// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Serilog;
using Serilog.Configuration;
#if NETSTANDARD
using Microsoft.AspNetCore.Http;
#endif

namespace Elastic.CommonSchema.Serilog;

public static class EnricherExtensions
{
#if NETSTANDARD
	/// <summary></summary>
	/// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
	/// <param name="httpContextAccessor"></param>
	/// <returns>Configuration object allowing method chaining.</returns>
	public static LoggerConfiguration WithEcsHttpContext(this LoggerEnrichmentConfiguration enrichmentConfiguration, IHttpContextAccessor httpContextAccessor)
	{
		if (enrichmentConfiguration == null)
			throw new ArgumentNullException(nameof(enrichmentConfiguration));
		return enrichmentConfiguration.With(new HttpContextEnricher(httpContextAccessor));
	}
#else
	public static LoggerConfiguration WithEcsHttpContext(this LoggerEnrichmentConfiguration enrichmentConfiguration)
	{
		if (enrichmentConfiguration == null)
			throw new ArgumentNullException(nameof(enrichmentConfiguration));
		return enrichmentConfiguration.With<HttpContextEnricher>();
	}
#endif
}