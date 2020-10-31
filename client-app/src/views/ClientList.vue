<template>
  <MainLayout>
    <template v-slot:icon>
      <v-icon @click="debugList = !debugList">mdi-card-account-details</v-icon>
    </template>
    <!--    <template v-slot:title>Список клиентов</template>-->
    <div v-if="debugList">
      <div :key="client.id" v-for="client in clientListInfo.value">
        {{ client }}
      </div>
    </div>
    <div align="center" v-else>
      <v-progress-circular indeterminate size="72" />
    </div>
    <div v-if="!clientListInfo.isLoading">
      <v-data-table
        :headers="headers"
        :items="clientListInfo.value"
        sort-by="id"
        class="elevation-4 ma-4 pa-4"
        disable-pagination
        hide-default-footer
      >
        <template #top>
          <v-toolbar flat>
            <v-toolbar-title>Клиенты</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="createDialog" max-width="800px">
              <template #activator="{on, attrs}">
                <v-btn color="primary" dark v-bind="attrs" v-on="on">
                  Новый клиент
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">Новый клиент</span>
                </v-card-title>
                <v-card-text>
                  <v-container>
                    <v-row justify="space-around">
                      <v-col cols="12" md="4">
                        <v-text-field
                          v-model="editedItem.fullName"
                          label="Имя"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="12" md="4">
                        <v-select
                          :items="tickets"
                          v-model="selectedTicket"
                          label="Путевка"
                          item-text="address"
                          return-object
                        ></v-select>
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="blue darken-1"
                    text
                    @click="createDialog = false"
                  >
                    Отмена
                  </v-btn>
                  <v-btn color="blue darken-1" text @click="postItem">
                    ОК
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template #item.fullName="props">
          <v-edit-dialog
            :return-value.sync="props.item.fullName"
            @save="putItem(props.item)"
          >
            {{ props.item.fullName }}
            <template #input>
              <v-text-field
                v-model="props.item.fullName"
                :rules="[v => v.length <= 25 || 'Input too long!']"
                label="Редактировать имя"
                single-line
                counter
              ></v-text-field>
            </template>
          </v-edit-dialog>
        </template>
        <template #item.info="{item}">
          {{ tickets.find(t => t.id === item.ticketId) }}
        </template>
        <template #item.actions="{item}">
          <v-icon small @click="deleteItem(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
      <v-dialog v-model="deleteDialog" max-width="600px">
        <v-card>
          <v-card-title class="headline"
            >Вы уверены, что хотите удалить этого клиента?</v-card-title
          >
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="deleteDialog = false"
              >Cancel</v-btn
            >
            <v-btn color="blue darken-1" text @click="deleteItemConfirm"
              >OK</v-btn
            >
            <v-spacer></v-spacer>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </MainLayout>
</template>

<script>
import MainLayout from "@/components/layout/MainLayout";
import moment from "moment";
export default {
  name: "ClientList",
  components: { MainLayout },
  data: () => ({
    debugList: false,
    headers: [
      { text: "Id", align: "start", value: "id" },
      { text: "ФИО", value: "fullName" },
      { text: "Информация", value: "info", sortable: false },
      { text: "Actions", value: "actions", sortable: false }
    ]
  }),
  created() {
    this.$store.dispatch("fetchClientListInfo");
    this.$store.dispatch("fetchTicketListInfo");
  },
  computed: {
    clientListInfo: {
      get() {
        return this.$store.state.client.clientListInfo;
      }
    },
    tickets: {
      get() {
        return this.$store.state.ticket.ticketListInfo.value;
      }
    },
    selectedTicket: {
      get() {
        return this.$store.state.client.selectedTicket;
      },
      set(value) {
        this.$store.dispatch("setSelectedTicket", { value: value });
      }
    },
    deleteDialog: {
      get() {
        return this.$store.state.client.deleteDialog;
      },
      set(value) {
        this.$store.dispatch("setDeleteDialog", { value: value });
      }
    },
    createDialog: {
      get() {
        return this.$store.state.client.createDialog;
      },
      set(value) {
        this.$store.dispatch("setCreateDialog", { value: value });
      }
    },
    editedItem: {
      get() {
        return this.$store.state.client.editedItem;
      },
      set(value) {
        this.$store.dispatch("setEditedItem", { item: value });
      }
    },
    editedItemTicketId: {
      get() {
        return this.$store.state.client.editedItem.ticketId;
      },
      set(value) {
        this.$store.dispatch("setEditedItemField", {
          fieldName: "ticketId",
          value: value
        });
      }
    }
  },
  methods: {
    moment(date) {
      return moment(date).format("LLL");
    },
    deleteItem(item) {
      this.editedItem = item;
      this.deleteDialog = true;
    },
    deleteItemConfirm() {
      this.$store.dispatch("deleteClient", {
        id: this.$store.state.client.editedItem.id
      });
      this.deleteDialog = false;
      this.clearFields();
    },
    putItem(item) {
      this.$store.dispatch("putClient", {
        id: item.id,
        data: item
      });
    },
    postItem() {
      this.editedItemTicketId = this.selectedTicket.id;
      this.$store.dispatch("postClient", { data: this.editedItem });
      this.createDialog = false;
      this.clearFields();
    },
    clearFields() {
      this.editedItem = Object.assign({}, this.$store.state.client.defaultItem);
      this.selectedTicket = Object.assign(
        {},
        this.$store.state.client.defaultTicket
      );
    }
  }
};
</script>

<style scoped></style>
