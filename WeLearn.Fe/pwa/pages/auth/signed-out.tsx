import { useEffect, useState } from "react"

import router from "next/router"
import { useSession } from "next-auth/react"

export default function SignedOutPage() {
    const { data: session, status } = useSession()
    const loading = status === "loading"
    const [redirectSeconds, setRedirectSeconds] = useState(3)

    useEffect(() => {
        if (redirectSeconds == 0) {
            router.push("/")
            return
        }

        setTimeout(() => {
            setRedirectSeconds((redirectSeconds) => redirectSeconds - 1);
        }, 1000)
    }, [redirectSeconds])

    return (
        <>
            <p>Successfully signed out.</p>
            <p>You will be redirected to the home page in {redirectSeconds}s.</p>
        </>
    )
}
