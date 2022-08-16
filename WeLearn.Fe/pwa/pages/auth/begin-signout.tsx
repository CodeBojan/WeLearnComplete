import { useEffect, useState } from "react"

import { useSession } from "next-auth/react"

function SignOutPage() {
    const { data: session, status } = useSession()
    const loading = status === "loading"
    const [content, setContent] = useState('')

    useEffect(() => {
        if (!session) return

        const isSignoutUrl = new URL(process.env.NEXT_PUBLIC_IS_SIGNOUT!)
        const postSignoutUrl = new URL(process.env.NEXT_PUBLIC_POST_SIGNOUT!)
        isSignoutUrl.searchParams.append("post_logout_redirect_uri", postSignoutUrl.href)
        isSignoutUrl.searchParams.append("id_token_hint", session.idToken as string) // TODO define interface
        window.location.assign(isSignoutUrl)
        setContent(isSignoutUrl.href)
    })

    return (
        <>{ }</>
    )
}

export default SignOutPage