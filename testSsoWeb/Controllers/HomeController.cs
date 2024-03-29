﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace testSsoWeb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // If the user is authenticated, then this is how you can get the access_token and id_token
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                ViewBag.AccessToken = accessToken;

                // if you need to check the access token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                var expiresAt = await HttpContext.GetTokenAsync("expires_at");
                var accessTokenExpiresAt = DateTime.Parse(expiresAt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                ViewBag.ExpiresAt = accessTokenExpiresAt;

                var idToken = await HttpContext.GetTokenAsync("id_token");
                ViewBag.IdToken = idToken;

                // Now you can use them. For more info on when and how to use the
                // access_token and id_token, see https://auth0.com/docs/tokens
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}