import Axios from "axios";

const state = {
  jewelryListInfo: { isLoading: false, value: [] }
};

const getters = {};

const mutations = {
  setJewelryListInfo(state, jewelryListInfo) {
    state.jewelryListInfo.value = jewelryListInfo;
    state.jewelryListInfo.isLoading = false;
  },
  setIsLoadingJewelry(state, { isLoading, fieldName }) {
    state[fieldName].isLoading = isLoading;
  }
};

const actions = {
  async fetchJewelryListInfo(context) {
    context.commit("setIsLoadingJewelry", {
      isLoading: true,
      fieldName: "jewelryListInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`/api/jewelries`)
        .then(response => {
          context.commit("setJewelryListInfo", response.data.result);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async putJewelryInfo(context, { id, data }) {
    return new Promise((resolve, reject) => {
      Axios.put(`/api/jewelry/${id}`, data)
        .then(() => {
          context.dispatch("fetchJewelryListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async deleteJewelryInfo(context, { id }) {
    return new Promise((resolve, reject) => {
      Axios.delete(`/api/jewelry/${id}`)
        .then(() => {
          context.dispatch("fetchJewelryListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async postJewelryInfo(context, { data }) {
    return new Promise((resolve, reject) => {
      Axios.post(`/api/jewelry/`, data)
        .then(() => {
          context.dispatch("fetchJewelryListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  }
};

export default { state, getters, mutations, actions };
