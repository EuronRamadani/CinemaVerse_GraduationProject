// axios
import axios from "axios";

export const subdomains = {
  database: process.env.VUE_APP_DATABASE_ENV,
  localhost: process.env.VUE_APP_ENV,
  node: process.env.NODE_API,
  movies: process.env.MOVIES_API,
};

export const axiosIns = {};
// eslint-disable-next-line no-restricted-syntax
Object.keys(subdomains).forEach((subdomain) => {
  axiosIns[subdomain] = axios.create({
    baseURL: subdomains[subdomain],
    headers: {
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods":
        "GET, POST, PATCH, PUT, DELETE, OPTIONS, HEAD",
      "Access-Control-Allow-Headers": "Origin, Content-Type, X-Auth-Token",
    },
  });
});

const api = (sub) => axiosIns[sub];

export default api;
