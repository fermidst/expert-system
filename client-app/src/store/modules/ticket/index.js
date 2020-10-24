import Axios from "axios";
import router from "@/router";

const state = {
  ticketInfo: { isLoading: false, value: null },
  ticketListInfo: { isLoading: false, value: [] }
};

const getters = {};

const mutations = {
  setTicketInfo(state, ticketInfo) {
    state.ticketInfo.value = ticketInfo;
    state.ticketInfo.isLoading = false;
  },
  setTicketListInfo(state, ticketListInfo) {
    state.ticketInfo.value = ticketListInfo;
    state.ticketInfo.isLoading = false;
  },
  setIsLoading(state, { isLoading, fieldName }) {
    state[fieldName].isLoading = isLoading;
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
  async fetchTicketInfo(context, id) {
    context.commit("setIsLoading", {
      isLoading: true,
      fieldName: "ticketInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`http://localhost:5000/ticket/${id}`)
        .then(response => {
          context.commit("setTicketInfo", response.data);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async fetchTicketListInfo(context) {
    context.commit("setIsLoading", {
      isLoading: true,
      fieldName: "clientListInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`http://localhost:5000/tickets`)
        .then(response => {
          context.commit("setTicketListInfo", response.data.result);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  }
};

export default { state, getters, mutations, actions };
