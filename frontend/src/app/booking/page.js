"use client";

import withAuth from "@/app/hoc/withAuth";
import LandingHeader from "@/app/components/landingHeader";
import BookingList from "@/app/components/bookingList";

function Booking() {
    return (
        <div className="pt-20">
            <LandingHeader/>
            <BookingList />
        </div>
    )
}

export default withAuth(Booking);