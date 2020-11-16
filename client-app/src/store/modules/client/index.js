import Axios from "axios";

const state = {
  clientListInfo: { isLoading: false, value: [] },
  editedItem: { id: 0, fullName: "", jewelryId: 0 },
  defaultItem: { id: 0, fullName: "", jewelryId: 0 },
  deleteDialog: false,
  createDialog: false
};

const getters = {};

const mutations = {
  setClientListInfo(state, clientListInfo) {
    state.clientListInfo.value = clientListInfo;
    state.clientListInfo.isLoading = false;
  },
  setIsLoadingClient(state, { isLoading, fieldName }) {
    state[fieldName].isLoading = isLoading;
  },
  setEditedIndex(state, editedIndex) {
    state.editedIndex = editedIndex;
  },
  setEditedItem(state, editedItem) {
    state.editedItem = Object.assign({}, editedItem);
  },
  setDeleteDialog(state, value) {
    state.deleteDialog = value;
  },
  setCreateDialog(state, value) {
    state.createDialog = value;
  },
  setEditedItemField(state, { fieldName, value }) {
    state.editedItem[fieldName] = value;
  }
};

const actions = {
  async fetchClientListInfo(context) {
    context.commit("setIsLoadingClient", {
      isLoading: true,
      fieldName: "clientListInfo"
    });
    return new Promise((resolve, reject) => {
      Axios.get(`/api/clients`)
        .then(response => {
          context.commit("setClientListInfo", response.data.result);
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async deleteClient(context, { id }) {
    return new Promise((resolve, reject) => {
      Axios.delete(`/api/client/${id}`)
        .then(() => {
          context.dispatch("fetchClientListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async putClient(context, { id, data }) {
    return new Promise((resolve, reject) => {
      Axios.put(`/api/client/${id}`, data)
        .then(() => {
          context.dispatch("fetchClientListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async postClient(context, { data }) {
    return new Promise((resolve, reject) => {
      Axios.post(`/api/client/`, data)
        .then(() => {
          context.dispatch("fetchClientListInfo");
          resolve();
        })
        .catch(e => {
          reject(e);
        });
    });
  },
  async setEditedIndex(context, { index }) {
    context.commit("setEditedIndex", index);
  },
  async setEditedItem(context, { item }) {
    context.commit("setEditedItem", item);
  },
  async setDeleteDialog(context, { value }) {
    context.commit("setDeleteDialog", value);
  },
  async setCreateDialog(context, { value }) {
    context.commit("setCreateDialog", value);
  },
  async setEditedItemField(context, { fieldName, value }) {
    context.commit("setEditedItemField", {
      fieldName: fieldName,
      value: value
    });
  }
};

export default { state, getters, mutations, actions };
