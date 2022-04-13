import NextAuth from "next-auth/next";
import IdentityServerProvider from 'next-auth/providers/identity-server4';

export default NextAuth({
    providers: [
        IdentityServerProvider({
            id: "identityServer",
            name: "Identity-Server",
            issuer: process.env.IS_ISSUER,
            authorization: { params: { scope: "openid profile" } }, // TODO add scopes
            clientId: process.env.IS_CLIENT_ID,
            clientSecret: process.env.IS_CLIENT_SECRET
        })
    ],
    callbacks: {
        async jwt({ token, account }) {
            console.log("token: " + JSON.stringify(token))
            console.log("account: " + JSON.stringify(account))
            if (account) {
                token.idToken = account.id_token;
                token.accessToken = account.access_token;
            }

            return token;
        },
        async session({ session, token, user }) {
            session.accessToken = token.accessToken;
            session.idToken = token.idToken;
            return session;
        }
    }
})