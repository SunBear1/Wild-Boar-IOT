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
  <h1 class="main-title">{{ TABLE_TITLE }}</h1>
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
        <td>Centro comerciala</td>
        <td>Francisco Chang</td>
        <td>Mexico</td>
      </tr>
    </table>
  </div>
</template>

<script setup lang="ts">
import { TABLE_TITLE, NAV_TABLE_TITLE, NAV_TITLE, NAV_DASHBOARD_TITLE} from "@/constants/texts";
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
