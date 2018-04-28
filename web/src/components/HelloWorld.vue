<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h2>d</h2>
    <input v-model="groupname">
    <button v-on:click="createGroup">Create Group</button>
    <br>
    {{ res }}
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'HelloWorld',
  data() {
    return {
      msg: 'YourTurnMyTurn',
      res: '',
      groupname: '',
    };
  },
  methods: {
    createGroup() {
      axios({ method: 'POST', url: 'http://localhost:56982/api/v1/group/' + this.groupname }).then((result) => {
        this.res = result.data;
        console.log(result);
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
