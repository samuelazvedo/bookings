import {PlusIcon} from "@heroicons/react/16/solid";

export default function Heading({ title, onCreate }) {
    return (
        <div className="flex flex-wrap items-center gap-6 sm:flex-nowrap">
            <h1 className="text-base font-semibold leading-7 text-gray-900">{title}</h1>
            <button
                onClick={onCreate}
                className="ml-auto flex items-center gap-x-1 rounded-md bg-red-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-red-600"
            >
                <PlusIcon className="-ml-1.5 h-5 w-5" aria-hidden="true" />
                Criar {title}
            </button>
        </div>
    );
}
