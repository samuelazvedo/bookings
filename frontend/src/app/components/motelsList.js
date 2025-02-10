"use client";

import { useEffect, useState } from "react";
import { getMotels } from "@/app/services/api";
import Heading from "@/app/components/heading";
import CreateDialog from "@/app/components/CreateDialog";
import DetailDialog from "@/app/components/DetailDialog";

export default function MotelsList() {
    const [motels, setMotels] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [detailDialogOpen, setDetailDialogOpen] = useState(false);
    const [selectedMotel, setSelectedMotel] = useState(null);
    const [createType, setCreateType] = useState("Motel");
    const [selectedMotelId, setSelectedMotelId] = useState(null);

    useEffect(() => {
        async function fetchMotels() {
            try {
                const data = await getMotels();
                setMotels(data);
                setLoading(false);
            } catch (error) {
                setError(error);
                setLoading(false);
            }
        }

        fetchMotels();
    }, []);

    if (loading) return <p className="text-center text-gray-600">Carregando motéis...</p>;
    if (error) return <p className="text-center text-red-500">Erro ao carregar os motéis.</p>;
    if (!motels || !motels.data) return <p className="text-center text-gray-500">Nenhum motel encontrado.</p>;

    return (
        <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                <Heading title="Motéis" onCreate={() => { setCreateType("Motel"); setDialogOpen(true); }} />

                <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
                    {motels.data.map((motel) => {
                        // Pega a primeira imagem ou usa um placeholder
                        const mainImage = motel.images?.[0]?.path || "https://placehold.co/600x400";

                        return (
                            <div key={motel.id} className="group relative p-4 border rounded-lg shadow-md bg-white">
                                <img
                                    alt={motel.name || "Imagem do motel"}
                                    src={mainImage}
                                    onClick={() => { setSelectedMotel(motel); setDetailDialogOpen(true); }}
                                    className="w-full h-40 object-cover rounded-md group-hover:opacity-75 cursor-pointer"
                                />
                                <div className="mt-4">
                                    <h3
                                        className="text-lg font-semibold text-gray-800 cursor-pointer hover:text-indigo-600"
                                        onClick={() => { setSelectedMotel(motel); setDetailDialogOpen(true); }}
                                    >
                                        {motel.name || "Motel sem nome"}
                                    </h3>
                                    <p className="text-sm text-gray-500">{motel.address || "Endereço não informado"}</p>

                                    {/* Miniaturas de imagens */}
                                    {motel.images?.length > 1 && (
                                        <div className="mt-2 flex gap-2 overflow-x-auto max-w-full py-2">
                                            {motel.images.slice(1).map((img, index) => (
                                                <img
                                                    key={index}
                                                    src={img.path}
                                                    alt="Imagem extra do motel"
                                                    className="w-12 h-12 rounded-md object-cover border shadow-sm shrink-0"
                                                />
                                            ))}
                                        </div>
                                    )}

                                    <button
                                        onClick={() => { setCreateType("Suíte"); setDialogOpen(true); setSelectedMotelId(motel.id) }}
                                        className="mt-2 py-1 text-sm font-medium text-indigo-600 hover:text-indigo-400">
                                        Adicionar Suíte
                                    </button>
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>

            {/* Create Dialog */}
            <CreateDialog isOpen={dialogOpen} onClose={() => setDialogOpen(false)} type={createType} motelId={selectedMotelId} />

            {/* Detail Dialog */}
            <DetailDialog isOpen={detailDialogOpen} onClose={() => setDetailDialogOpen(false)} motel={selectedMotel} />
        </div>
    );
}
