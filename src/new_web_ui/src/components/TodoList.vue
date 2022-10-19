<template>
    <div>
        {{ TITLE }}
        <button @click="AddToCounter"> {{ clickCount }} </button>
        <button @click="ReverseName">Reverse Task Name</button>
        <span> {{ task.title }} </span>
        <div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue';
import type { Ref } from 'vue';
interface Task {
    title: String,
    done: Boolean,
}
const TITLE = 'Todo list';
const clickCount: Ref<number> = ref(0);
const task: Task = reactive({
    title: '1st Task',
    done: false,
});

const AddToCounter = () => {
    clickCount.value += 1;
}
const ReverseName = () => {
    task.title = task.title.split("").reverse().join("");
}
</script>

<style scoped>

</style>
