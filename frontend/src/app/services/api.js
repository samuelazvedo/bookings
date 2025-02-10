import axios from "axios";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7009/api";

const api = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});

api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("token");
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

export const login = async (email, password) => {
    const response = await api.post("/auth/login", { email, password });

    if (response.data.token) {
        localStorage.setItem("token", response.data.token);
    }

    return response.data;
};

export const register = async (name, email, PasswordHash) => {
    const response = await api.post("/auth/register", { name, email, PasswordHash });
    return response.data;
};

export const getUser = async () => {
    try {
        const response = await api.get("/auth/user");
        return response.data;
    } catch (error) {
        console.error("Erro ao buscar usuário:", error);
        throw error;
    }
};

export const logout = () => {
    localStorage.removeItem("token");
};

export const getBookings = async (page = 1, pageSize = 10, startDate = null, endDate = null) => {
    try {
        const params = new URLSearchParams({
            page,
            pageSize,
        });

        if (startDate) params.append("startDate", startDate.toISOString().split("T")[0]);
        if (endDate) params.append("endDate", endDate.toISOString().split("T")[0]);

        const response = await api.get(`/bookings?${params.toString()}`);
        return response.data;
    } catch (error) {
        console.error("Erro ao buscar reservas:", error);
        throw error;
    }
};


export const getMotels = async () => {
    try {
        const response = await api.get("/motels");
        return response.data;
    } catch (error) {
        console.error("Erro ao buscar motéis:", error);
        throw error;
    }
};

export const getRevenue = async () => {
    const response = await api.get("/revenue");
}

export const createMotel = async (motelData) => {
    return api.post("/motels", motelData);
};

export const createSuite = async (suiteData) => {
    return api.post("/suites", suiteData);
};

export const createBooking = async (bookingData) => {
    return api.post("/bookings", bookingData);
};

export default api;
