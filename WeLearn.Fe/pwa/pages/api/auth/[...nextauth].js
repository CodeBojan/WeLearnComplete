import { Console } from "console";
import IdentityServerProvider from "next-auth/providers/identity-server4";
import NextAuth from "next-auth/next";

const refreshAccessTokenError = "RefreshAccessTokenError";

const getIsIssuer = () => process.env.IS_ISSUER;
const getClientId = () => process.env.IS_CLIENT_ID;
const getClientSecret = () => process.env.IS_CLIENT_SECRET;
const getScopes = () => process.env.IS_SCOPES;

async function refreshAccessToken(token) {
  try {
    const refreshUrl = `${process.env.IS_ISSUER}/connect/token`;

    const body = new URLSearchParams({
      client_id: getClientId(),
      client_secret: getClientSecret(),
      grant_type: "refresh_token",
      refresh_token: token.refreshToken,
    });

    refreshUrl;

    const response = await fetch(refreshUrl, {
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      method: "POST",
      body: body,
    });

    const refreshedTokens = await response.json();

    if (!response.ok) {
      throw refreshedTokens;
    }

    return {
      ...token,
      accessToken: refreshedTokens.access_token,
      accessTokenExpires: Date.now() + refreshedTokens.expires_in * 1000,
      refreshToken: refreshedTokens.refresh_token ?? token.refreshToken,
    };
  } catch (error) {
    console.error(error);

    return {
      ...token,
      error: refreshAccessTokenError,
    };
  }
}

export default NextAuth({
  providers: [
    IdentityServerProvider({
      id: "identityServer",
      name: "Identity-Server",
      issuer: getIsIssuer(),
      authorization: { params: { scope: getScopes() } },
      clientId: getClientId(),
      clientSecret: getClientSecret(),
    }),
  ],
  callbacks: {
    async jwt({ token, user, account, profile, isNewUser }) {
      if (account && user) {
        return {
          idToken: account.id_token,
          accessToken: account.access_token,
          accessTokenExpires: account.expires_at * 1000,
          refreshToken: account.refresh_token,
          user,
        };
      }

      if (Date.now() >= token.accessTokenExpires) {
        return refreshAccessToken(token);
      }

      return token;
    },
    async session({ session, token, user }) {
      session.user = token.user;
      session.error = token.error;
      session.accessToken = token.accessToken;
      session.idToken = token.idToken;
      return session;
    },
  },
});
