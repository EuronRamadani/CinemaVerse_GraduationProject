import firebase from "firebase/app";
import "firebase/firestore";

const firebaseConfig = {
  apiKey: "AIzaSyAnYLc54geuQzqysf2161E0ZmLH7-GKo44",
  authDomain: "cinemaverse-13e76.firebaseapp.com",
  projectId: "cinemaverse-13e76",
  storageBucket: "cinemaverse-13e76.appspot.com",
  messagingSenderId: "502334150744",
  appId: "1:502334150744:web:17548aaaf23dabaf81922d",
};

const fireabaseApp = firebase.initializeApp(firebaseConfig);
const timestamp = firebase.firestore.FieldValue.serverTimestamp;

const getAuth = firebase.auth();
export { timestamp, getAuth };
export default fireabaseApp.firestore();