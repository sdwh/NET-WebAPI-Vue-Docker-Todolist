<template>
    <table class="table">
        <thead>
            <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Description</th>
                <th>Due Date</th>
                <th>Category</th>
                <th class="text-success">
                    <span class="material-icons fs-3" style="cursor: pointer;" @click="addTodoItem()">add</span>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="todo in todos" :key="todo.todoItemID" @click="editTodoItem(todo)">
                <td>{{ todo.todoItemID }}</td>
                <td>{{ todo.title }}</td>
                <td>{{ todo.description }}</td>
                <td>{{ todo.dueDate }}</td>
                <td>{{ todo.category ? todo.category.categoryName : 'N/A' }}</td>
                <td class="text-danger">
                    <button @click="deleteTodoItem(todo)" class="btn btn-danger">Delete</button>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    export default {
        props: {
            todos: Array,
        },
        methods: {
            editTodoItem(todo) {
                this.$emit('edit-todo', todo);
            },
            addTodoItem() {
                this.$emit('add-todo');
            },
            deleteTodoItem(todo) {
                this.$emit('delete-todo', todo);
            },
        },
    };
</script>