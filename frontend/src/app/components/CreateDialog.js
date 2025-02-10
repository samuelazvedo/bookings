'use client';

import { useState } from 'react';
import { Dialog } from '@headlessui/react';
import { createMotel, createSuite, createBooking } from '@/app/services/api';

export default function CreateDialog({ isOpen, onClose, type, motelId }) {
    const [formData, setFormData] = useState({
        name: '',
        basePrice: '',
        motelId: motelId || ''
    });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (type === 'Motel') {
                await createMotel({ name: formData.name });
            } else if (type === 'Suíte') {
                await createSuite({
                    suiteName: formData.name,
                    basePrice: parseFloat(formData.basePrice),
                    motelId: motelId
                });
            } else if (type === 'Reserva') {
                await createBooking({ name: formData.name, motelId: motelId });
            }
            onClose();
        } catch (error) {
            console.error(`Erro ao criar ${type}:`, error);
        }
    };

    return (
        <Dialog open={isOpen} onClose={onClose} className="relative z-10">
            <div className="fixed inset-0 bg-gray-500/75 transition-opacity" />
            <div className="fixed inset-0 z-10 w-screen overflow-y-auto">
                <div className="flex min-h-full items-center justify-center p-4 text-center">
                    <Dialog.Panel className="w-full max-w-lg overflow-hidden rounded-lg bg-white shadow-xl">
                        <div className="px-6 py-4">
                            <Dialog.Title className="text-lg font-semibold text-gray-900">
                                Criar {type}
                            </Dialog.Title>
                            <form onSubmit={handleSubmit} className="mt-4 space-y-4">
                                <input
                                    type="text"
                                    name="name"
                                    placeholder="Nome"
                                    value={formData.name}
                                    onChange={handleChange}
                                    className="w-full rounded-md border-gray-300 px-3 py-2 text-gray-900 shadow-sm focus:border-red-500 focus:ring-red-500"
                                    required
                                />
                                {type === 'Suíte' && (
                                    <input
                                        type="number"
                                        name="basePrice"
                                        placeholder="Preço Base"
                                        value={formData.basePrice}
                                        onChange={handleChange}
                                        className="w-full rounded-md border-gray-300 px-3 py-2 text-gray-900 shadow-sm focus:border-red-500 focus:ring-red-500"
                                        required
                                    />
                                )}
                                <div className="flex justify-end space-x-2">
                                    <button type="button" onClick={onClose} className="px-4 py-2 text-gray-700 hover:bg-gray-100 rounded-md">
                                        Cancelar
                                    </button>
                                    <button type="submit" className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-500">
                                        Criar
                                    </button>
                                </div>
                            </form>
                        </div>
                    </Dialog.Panel>
                </div>
            </div>
        </Dialog>
    );
}
