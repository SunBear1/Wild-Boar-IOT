<template>
  <nav class="navbar">
    <h3> {{ NAV_TITLE }}</h3>
    <button>
      <span> {{ NAV_DASHBOARD_TITLE }}</span>
    </button>
    <button>
      <span>{{ NAV_TABLE_TITLE }}</span>
    </button>
  </nav>
  <br>
  <h1 class="main-title">{{ TABLE_TITLE }}</h1>
  <div>
    <table class="table-root">
      <tr>
        <th>ID</th>
        <th>Occupancy</th>
        <th>Weight</th>
      </tr>
      <tr v-for="row in rows">
        <td>{{ row.id }}</td>
        <td>{{ row.occupancy }}</td>
        <td>{{ row.weight }}</td>
      </tr>
    </table>
  </div>
</template>

<script setup lang="ts">
import {NAV_DASHBOARD_TITLE, NAV_TABLE_TITLE, NAV_TITLE, TABLE_TITLE} from "@/constants/texts";
import {onMounted, ref} from 'vue'
import {get_sensor_data_from_api} from "@/controller/sensor_data";

let rows = ref()
let tmp

onMounted(async () => {
  tmp = await get_sensor_data_from_api()
  rows.value = tmp
  console.log(rows.value)
})


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
    border-collapse: separate;
    border-spacing: 2px;
    border: 1px solid black;
  }
  th, tr{
    border: 1px solid black;
  }
  td {
    padding: 6px;
    border: 1px solid black;
  }
  .table-column{
    border: 1px solid black;
  }
  .navbar {
    display: flex;
    align-items: center;
  }
</style>
