"use client";

import { useState, useEffect } from "react";
import { ArrowDownIcon, ArrowUpIcon } from "@heroicons/react/20/solid";
import api from "../services/api";

const classNames = (...classes) => classes.filter(Boolean).join(" ");

export default function RevenueStats() {
    const [stats, setStats] = useState([
        { name: "Total", stat: "-", change: "-", changeType: "increase" },
        { name: "Mensal", stat: "-", change: "-", changeType: "increase" },
        { name: "Semanal", stat: "-", change: "-", changeType: "increase" }
    ]);
    const [selectedDate, setSelectedDate] = useState("");
    const [dailyRevenue, setDailyRevenue] = useState(null);

    useEffect(() => {
        fetchRevenue();
    }, []);

    const fetchRevenue = async (date = null) => {
        try {
            const response = await api.get("/revenue", { params: date ? { date } : {} });

            if (!response.data) {
                console.error("Erro: Resposta da API está vazia ou inválida.");
                return;
            }

            if (date) {
                setDailyRevenue(response.data.revenue ?? 0);
            } else {
                setStats([
                    {
                        name: "Total",
                        stat: (response.data.total ?? 0).toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        }),
                        change: `${response.data.totalChange ?? 0}%`,
                        changeType: response.data.totalChange > 0 ? "increase" : "decrease"
                    },
                    {
                        name: "Mensal",
                        stat: (response.data.monthly ?? 0).toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        }),
                        change: `${response.data.monthlyChange ?? 0}%`,
                        changeType: response.data.monthlyChange > 0 ? "increase" : "decrease"
                    },
                    {
                        name: "Semanal",
                        stat: (response.data.weekly ?? 0).toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL"
                        }),
                        change: `${response.data.weeklyChange ?? 0}%`,
                        changeType: response.data.weeklyChange > 0 ? "increase" : "decrease"
                    }
                ]);
            }
        } catch (error) {
            console.error("Erro ao buscar os dados de faturamento:", error);
        }
    };


    const handleDateChange = async (event) => {
        const date = event.target.value;
        setSelectedDate(date);

        if (date) {
            try {
                const response = await api.get("/revenue", { params: { date } });
                setDailyRevenue(response.data?.revenue ?? 0);
            } catch (error) {
                console.error("Erro ao buscar o faturamento diário:", error);
                setDailyRevenue(0);
            }
        } else {
            setDailyRevenue(null);
        }
    };

    return (
        <div className="px-4 sm:px-6 lg:px-8 mt-20">
            <h3 className="text-base font-semibold leading-6 text-gray-900">Faturamento</h3>
            <input
                type="date"
                value={selectedDate}
                onChange={handleDateChange}
                className="mt-4 p-2 border border-gray-200 text-sm rounded-md text-gray-900"
            />
            {dailyRevenue !== null && (
                <div className="mt-4 text-lg font-semibold text-gray-600">
                    Faturamento diário:{" "}
                    {(dailyRevenue ?? 0).toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}
                </div>
            )}
            <dl className="mt-5 grid grid-cols-1 divide-y divide-gray-200 overflow-hidden rounded-lg bg-white shadow md:grid-cols-3 md:divide-x md:divide-y-0">
                {stats.map((item) => (
                    <div key={item.name} className="px-4 py-5 sm:p-6">
                        <dt className="text-base font-normal text-gray-900">{item.name}</dt>
                        <dd className="mt-1 flex items-baseline justify-between md:block lg:flex">
                            <div className="flex items-baseline text-2xl font-semibold text-gray-600">
                                {item.stat}
                            </div>
                            <div
                                className={classNames(
                                    item.changeType === "increase" ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800",
                                    "inline-flex items-baseline rounded-full px-2.5 py-0.5 text-sm font-medium md:mt-2 lg:mt-0"
                                )}
                            >
                                {item.changeType === "increase" ? (
                                    <ArrowUpIcon
                                        className="-ml-1 mr-0.5 h-5 w-5 flex-shrink-0 self-center text-green-500"
                                        aria-hidden="true"
                                    />
                                ) : (
                                    <ArrowDownIcon
                                        className="-ml-1 mr-0.5 h-5 w-5 flex-shrink-0 self-center text-red-500"
                                        aria-hidden="true"
                                    />
                                )}
                                <span
                                    className="sr-only"> {item.changeType === "increase" ? "Aumentou" : "Diminuiu"} de </span>
                                {item.change}
                            </div>
                        </dd>
                    </div>
                ))}
            </dl>
        </div>
    );
}
