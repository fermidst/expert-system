import Axios from "axios";

const state = {
  ticketListInfo: { isLoading: false, value: [] }
};

const getters = {};

const mutations = {
  setTicketListInfo(state, ticketListInfo) {
    state.ticketListInfo.value = ticketListInfo;
    state.ticketListInfo.isLoading = false;
  },
  setIsLoadingTicket(state, { isLoading, fieldName }) {
    state[fieldName].isLoading = isLoading;
  }
};

const actions = {
  async fetchTicketListInfo(context) {
    context.commit("setIsLoadingTicket", {
      isLoading: true,
      fieldName: "ticketListInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`/api/tickets`)
        .then(response => {
          context.commit("setTicketListInfo", response.data.result);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async putTicketInfo(context, { id, data }) {
    return new Promise((resolve, reject) => {
      Axios.put(`/api/ticket/${id}`, data)
        .then(() => {
          context.dispatch("fetchTicketListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async deleteTicketInfo(context, { id }) {
    return new Promise((resolve, reject) => {
      Axios.delete(`/api/ticket/${id}`)
        .then(() => {
          context.dispatch("fetchTicketListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async postTicketInfo(context, { data }) {
    return new Promise((resolve, reject) => {
      Axios.post(`/api/ticket/`, data)
        .then(() => {
          context.dispatch("fetchTicketListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  }
};

export default { state, getters, mutations, actions };
