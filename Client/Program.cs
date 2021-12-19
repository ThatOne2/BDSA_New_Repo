using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TrialProject.Client;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("TrialProject.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


// Supply HttpClient instances that include access tokens when making requests to the server project

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TrialProject.ServerAPI"));
builder.Services.AddScoped(sp => new HttpClient{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

/*
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://890643f7-4176-462f-90f9-5ce4ec82e63d/API.Access");
"api://862c81f0-91e8-476e-8646-b7c297967ec9/BlazorHostedAPI.Access"
});
*/

builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://890643f7-4176-462f-90f9-5ce4ec82e63d/API.Access");
    options.UserOptions.RoleClaim = "appRole";
})
.AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount,
    CustomAccountFactory>();

await builder.Build().RunAsync();
  