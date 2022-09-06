import axios from "axios";

const getFilms = async () => {
	const apiCaller = axios.create({
		baseURL: "https://localhost:44355/",
	});
	const { data } = await apiCaller.get(`api/movies/`);

	return data;
};

export default getFilms;
