"use client";

import withAuth from "@/app/hoc/withAuth";
import LandingHeader from "@/app/components/landingHeader";
import RevenueStats from "@/app/components/RevenueStats";

function Revenue() {
    return (
        <div className="pt-20">
            <LandingHeader/>
            <RevenueStats />
        </div>
    )
}

export default withAuth(Revenue);