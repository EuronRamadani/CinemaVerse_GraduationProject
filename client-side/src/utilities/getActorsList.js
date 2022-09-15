import axios from "axios";

const getActors = async () => {
  const apiCaller = axios.create({
    baseURL: "https://localhost:44355/",
  });
  const { data } = await apiCaller.get(`api/actors/`);

  return data;
};

export default getActors;
