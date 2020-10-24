import router from "@/router";
import Axios from "axios";

const state = {
  clientInfo: { isLoading: false, value: null },
  clientListInfo: { isLoading: false, value: [] }
};

const getters = {};

const mutations = {
  setClientInfo(state, clientInfo) {
    state.clientInfo.value = clientInfo;
    state.clientInfo.isLoading = false;
  },
  setClientListInfo(state, clientListInfo) {
    state.clientListInfo.value = clientListInfo;
    state.clientListInfo.isLoading = false;
  },
  setIsLoading(state, { isLoading, fieldName }) {
    state[fieldName].isLoading = isLoading;
  },
  setClientListInfoIsLoading(state, { isLoading }) {
    state.clientListInfo.isLoading = isLoading;
  }
};

const actions = {
  login(context, { name, password }) {
    if (name === "admin" && password === "admin") {
      context.commit("setLoginStatus", true);
      router.push("/");
    } else alert("Неправильный логин или пароль");
  },
  logout(context) {
    context.commit("setLoginStatus", false);
    router.push("/login");
  },
  async fetchClientInfo(context, { id }) {
    context.commit("setIsLoading", {
      isLoading: true,
      fieldName: "clientInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`http://localhost:5000/client/${id}`)
        .then(response => {
          context.commit("setClientInfo", response.data);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async fetchClientListInfo(context) {
    context.commit("setClientListInfoIsLoading", {
      isLoading: true
    });
    return new Promise((resolve, reject) => {
      Axios.get(`http://localhost:5000/clients`)
        .then(response => {
          context.commit("setClientListInfo", response.data.result);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  }
};

export default { state, getters, mutations, actions };
