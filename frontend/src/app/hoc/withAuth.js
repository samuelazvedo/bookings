"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

const withAuth = (WrappedComponent) => {
    return function AuthenticatedComponent(props) {
        const router = useRouter();
        const [loading, setLoading] = useState(true);

        useEffect(() => {
            const token = localStorage.getItem("token");

            if (!token) {
                router.replace("/login"); // Redireciona para login se não houver token
            } else {
                setLoading(false);
            }
        }, [router]);

        if (loading) {
            return null; // Evita piscar conteúdo antes do redirecionamento
        }

        return <WrappedComponent {...props} />;
    };
};

export default withAuth;
