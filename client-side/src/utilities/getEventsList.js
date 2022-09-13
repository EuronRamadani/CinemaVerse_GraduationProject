import axios from "axios";

const getEvents = async () => {
  const apiCaller = axios.create({
    baseURL: "https://localhost:44355/",
  });
  const { data } = await apiCaller.get(`api/events/`);

  return data;
};

export default getEvents;
