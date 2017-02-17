// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.AspNetCore.Mvc.FunctionalTests
{
    // Test to verify compilation options from the application are used to compile
    // precompiled and dynamically compiled views.
    public class CompilationOptionsTests : IClassFixture<MvcTestFixture<RazorWebSite.Startup>>
    {
        public CompilationOptionsTests(MvcTestFixture<RazorWebSite.Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task CompilationOptions_AreUsedByViewsAndPartials()
        {
            // Arrange
#if NET461
            var expected =
@"This method is running from NET461
This method is only defined in NET461";
#elif NETCOREAPP1_1
            var expected =
@"This method is running from NETCOREAPP1_1
This method is only defined in NETCOREAPP1_1";
#endif

            // Act
            var body = await Client.GetStringAsync("http://localhost/ViewsConsumingCompilationOptions/");

            // Assert
            Assert.Equal(expected, body.Trim(), ignoreLineEndingDifferences: true);
        }
    }
}
