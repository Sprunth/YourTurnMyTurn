<template>
 <el-container>
   <el-header>
    <h1>{{ msg }}</h1>
   </el-header>
   <el-main>
    <h2>Selected Group: {{ groupname }}</h2>
    <el-input v-model="groupname"/>
    <el-button v-on:click="createGroup">Create Group</el-button>
    <br>
    {{ groupid }}
    <br><br>
    <el-input v-model="personname"/>
    <el-button v-on:click="createPerson">Create Person</el-button>
    <br>
    <br>
    <h2>Group Members</h2>
    <el-button v-on:click="fetchGroupMembers">Fetch</el-button>
    <br><br>
    <table style="align: ">
      <thead>
        <th>Name</th><th>Contributed Value</th>
      </thead>
      <tbody>
        <tr v-for="gp in groupPersons" :key="gp.id">
          <td>{{ gp.name }}</td>
          <td>{{ gp.contributedValue }}</td>
        </tr>
      </tbody>
    </table>
   </el-main>
  </el-container>
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
      axios({ method: 'POST', url: `http://localhost:56982/api/v1/group/${this.groupname}` }).then((result) => {
        this.groupid = result.data.slice(7); // ugh
        console.log('createGroup', result.data);
      }, (error) => {
        console.error(error);
      });
    },
    createPerson() {
      axios({ method: 'POST', url: `http://localhost:56982/api/v1/person/${this.personname}` }).then((result) => {
        console.log('createPerson', result.data);
        this.addPersonToGroup(result.data.slice(8));
        this.fetchGroupMembers(); // todo: fix this race condition. then/finally/chain?
      }, (error) => {
        console.error(error);
      });
    },
    addPersonToGroup(personId) {
      axios({ method: 'POST', url: `http://localhost:56982/api/v1/group/${this.groupid}/add/${personId}` }).then((result) => {
        console.log('addPersonToGroup', result.data);
      }, (error) => {
        console.error(error);
      });
    },
    fetchGroupMembers() {
      axios({ method: 'GET', url: `http://localhost:56982/api/v1/group/${this.groupid}` }).then((result) => {
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

table {
  border: 2px solid #42b983;
  border-radius: 3px;
  background-color: #fff;
  margin-left: auto;
  margin-right: auto;
}

th {
  background-color: #42b983;
  color: rgba(255,255,255,0.66);
  cursor: pointer;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

td {
  background-color: #f9f9f9;
}

th, td {
  min-width: 120px;
  padding: 10px 20px;
}

</style>
