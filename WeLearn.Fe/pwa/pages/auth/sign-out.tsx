import { signOut, useSession } from "next-auth/react"
import { useEffect, useState } from "react"

export default function SignedOutPage() {
    const { data: session, status } = useSession()
    const loading = status === "loading"
    const [content, setContent] = useState()

    useEffect(() => {
        signOut({ callbackUrl: "/auth/signed-out" });
    })

    return (
        <></>
    )
}
