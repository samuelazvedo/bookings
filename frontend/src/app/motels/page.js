"use client";

import LandingHeader from "@/app/components/landingHeader";
import MotelsList from "@/app/components/motelsList";
import withAuth from "@/app/hoc/withAuth";

function Motel() {
    return (
        <div className="pt-20">
            <LandingHeader />
            <MotelsList />
        </div>
    );
}

export default withAuth(Motel);
