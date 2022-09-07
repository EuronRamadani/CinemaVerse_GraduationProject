import axios from "axios";

const getMovies = async () => {
  const apiCaller = axios.create({
    baseURL: "https://localhost:44355/",
  });
  const { data } = await apiCaller.get(`api/movies/`);

  return data;
};

export default getMovies;
