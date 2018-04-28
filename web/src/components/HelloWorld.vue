<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h2>d</h2>
    <input v-model="groupname">
    <button v-on:click="createGroup">Create Group</button>
    <br>
    {{ groupid }}
    <br><br>
    <input v-model="personname">
    <button v-on:click="createPerson">Create Person</button>
    <br>
    <br>
    <h2>Group Members {{ groupname }}</h2>
    <button v-on:click="fetchGroupMembers">Fetch</button>
    <ol>
      <li v-for="gp in groupPersons" :key="gp.id">
        {{ gp }}
      </li>
    </ol>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'HelloWorld',
  data() {
    return {
      msg: 'YourTurnMyTurn',
      groupid: '',
      groupname: '',
      personname: '',
      groupPersons: [],
    };
  },
  methods: {
    createGroup() {
      axios({ method: 'POST', url: 'http://localhost:56982/api/v1/group/' + this.groupname }).then((result) => {
        this.groupid = result.data.slice(7); // ugh
        console.log('createGroup', result.data);
      }, (error) => {
        console.error(error);
      });
    },
    createPerson() {
      axios({ method: 'POST', url: 'http://localhost:56982/api/v1/person/' + this.personname }).then((result) => {
        console.log('createPerson', result.data);
        this.addPersonToGroup(result.data.slice(8));
        this.fetchGroupMembers();
      }, (error) => {
        console.error(error);
      });
    },
    addPersonToGroup(personId) {
      axios({ method: 'POST', url: 'http://localhost:56982/api/v1/group/' + this.groupid + '/add/' + personId }).then((result) => {
        console.log('addPersonToGroup', result.data);
      }, (error) => {
        console.error(error);
      });
    },
    fetchGroupMembers(){
      axios({ method: 'GET', url: 'http://localhost:56982/api/v1/group/' + this.groupid }).then((result) => {
        this.groupPersons = result.data;
        console.log('fetchGroupMembers', result.data);
      }, (error) => {
        console.error(error);
      });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
