<template>
  <nav class="navbar">
    <h3>Wild boar IOT</h3>
    <button>
      <span>Dashboard</span>
    </button>
    <button>
      <span>Sensor data</span>
    </button>
  </nav>
  <h1 class="main-title">{{ TITLE }}</h1>
  <div>
    <table class="table-root">
      <tr>
        <th>Company</th>
        <th>Contact</th>
        <th>Country</th>
      </tr>
      <tr>
        <td>Alfreds Futterkiste</td>
        <td>Maria Anders</td>
        <td>Germany</td>
      </tr>
      <tr>
        <td>Centro comercial Moctezuma</td>
        <td>Francisco Chang</td>
        <td>Mexico</td>
      </tr>
    </table>
  </div>
</template>

<script setup lang="ts">
const TITLE: string = "Data from sensors";

</script>


<style scoped>
  .main-title{
    margin-top: 10px;
    text-align: center;
  }
  .table-root{
    margin-top: 50px;
    margin-left:auto;
    margin-right:auto;
  }
  .navbar {
    display: flex;
    align-items: center;
  }
</style>