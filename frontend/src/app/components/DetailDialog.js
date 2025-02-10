"use client";

import { useState, useEffect } from "react";
import { Dialog } from "@headlessui/react";

export default function DetailDialog({ isOpen, onClose, motel }) {
    const [selectedImage, setSelectedImage] = useState(null);

    useEffect(() => {
        if (isOpen && motel?.images?.length > 0) {
            setSelectedImage(motel.images[0].path);
        }
    }, [isOpen, motel]);

    if (!motel) return null;

    return (
        <Dialog open={isOpen} onClose={onClose} className="fixed inset-0 z-50 flex items-center justify-center p-4">
            <div className="fixed inset-0 bg-gray-500/75 z-40 transition-opacity" />

            <Dialog.Panel className="relative z-50 w-full max-w-3xl h-[80vh] bg-white rounded-lg shadow-xl p-6 flex flex-col">
                <Dialog.Title className="text-lg font-semibold text-gray-900 mb-4">
                    {motel.name}
                </Dialog.Title>

                <div className="flex-1 overflow-y-auto">
                    <div className="w-full flex justify-center">
                        {selectedImage ? (
                            <img
                                src={selectedImage}
                                alt="Imagem principal do motel"
                                className="w-full max-h-72 object-cover rounded-lg shadow-md"
                            />
                        ) : (
                            <p className="text-center text-gray-500">Nenhuma imagem disponível</p>
                        )}
                    </div>

                    {motel.images?.length > 1 && (
                        <div className="mt-4 flex gap-2 overflow-x-auto">
                            {motel.images.map((img, index) => (
                                <img
                                    key={index}
                                    src={img.path}
                                    alt={`Miniatura ${index + 1}`}
                                    onClick={() => setSelectedImage(img.path)}
                                    className={`w-16 h-16 object-cover cursor-pointer border rounded-md shadow-sm ${
                                        selectedImage === img.path ? "border-indigo-500" : "border-gray-300"
                                    }`}
                                />
                            ))}
                        </div>
                    )}

                    <div className="mt-6">
                        <p className="text-gray-600">{motel.address}</p>
                        {motel.suites?.length > 0 && (
                            <>
                                <h4 className="mt-4 font-semibold text-gray-900">Suítes Disponíveis:</h4>
                                <ul className="mt-2 space-y-1">
                                    {motel.suites.map((suite) => (
                                        <li key={suite.id} className="text-sm text-gray-700">
                                            <strong>{suite.suiteName}</strong> - R$ {suite.basePrice?.toFixed(2)}
                                        </li>
                                    ))}
                                </ul>
                            </>
                        )}
                    </div>
                </div>

                <div className="mt-4 flex justify-end border-t pt-4">
                    <button
                        onClick={onClose}
                        className="px-4 py-2 bg-gray-700 text-white rounded-md hover:bg-gray-600"
                    >
                        Fechar
                    </button>
                </div>
            </Dialog.Panel>
        </Dialog>
    );
}
