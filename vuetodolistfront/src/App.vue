<template>
    <div class="container">
        <div class="row text-center my-4">
            <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />
            <h1>Vue.js Todolist</h1>
        </div>
        <div class="row">
            <div class="col-md-4 p-3">
                <Form :postDataObj="postDataObj"
                      :editMode="editMode"
                      :categories="categories"
                      @init-post="initPostData"
                      @load-todo="loadTodos" />
            </div>
            <div class="col-md-8">
                <Table :todos="todos" @edit-todo="editTodoItem" @add-todo="addTodoItem" @delete-todo="deleteTodoItem" />
            </div>
        </div>
    </div>
</template>

<script>
    import Form from './components/Form.vue';
    import Table from './components/Table.vue';

    export default {
        components: {
            Form,
            Table,
        },
        data() {
            return {
                postDataObj: {
                    title: '',
                    description: '',
                    dueDate: this.getDatetimeNow(),
                },
                editMode: false,
                todos: [],
                categories: [],
            };
        },
        mounted() {
            this.loadTodos();
            this.loadCategories();
            this.initPostData();
        },
        methods: {
            getDatetimeNow() {
                return new Date().toISOString().slice(0, 16);
            },
            initPostData() {
                this.postDataObj = {
                    title: '',
                    description: '',
                    dueDate: this.getDatetimeNow(),
                };
                document.getElementById('title').focus();
            },
            loadTodos() {
                fetch(`${import.meta.env.VITE_API_BASE_URL}/api/TodoItems/`, { method: 'GET' })
                    .then(response => response.json())
                    .then(data => {
                        this.todos = data;
                    })
                    .catch(error => {
                        console.error('Error fetching data:', error);
                    });
            },
            loadCategories() {
                fetch(`${import.meta.env.VITE_API_BASE_URL}/api/Categories/`)
                    .then(response => response.json())
                    .then(data => {
                        this.categories = data.map(item => item);
                    })
                    .catch(error => {
                        console.error('Error fetching data:', error);
                    });
            },
            editTodoItem(todo) {
                this.postDataObj.title = todo.title;
                this.postDataObj.description = todo.description;
                this.postDataObj.dueDate = todo.dueDate;
                this.postDataObj.categoryId = todo.category ? todo.category.categoryID : null;
                this.postDataObj.TodoItemID = todo.todoItemID;

                // Change the button label and action
                this.editMode = true;
            },
            addTodoItem() {
                this.editMode = false;
                this.initPostData();
            },
            deleteTodoItem(todo) {
                const confirmDelete = confirm("Are you sure you want to delete this item?");
                if (confirmDelete) {
                    fetch(`${import.meta.env.VITE_API_BASE_URL}/api/TodoItems/${todo.todoItemID}`, {
                        method: 'DELETE'
                    })
                        .then(response => {
                            if (response.ok) {
                                this.loadTodos();
                                this.initPostData();
                                console.log('Successfully deleted todo item.');
                            } else {
                                console.error('Failed to delete todo item.');
                            }
                        })
                        .catch(error => {
                            console.error('Error deleting todo item:', error);
                        });
                }
            },
            postData() {
                const requestOptions = {
                    method: this.editMode ? 'PUT' : 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.postDataObj)
                };

                const url = this.editMode
                    ? `${import.meta.env.VITE_API_BASE_URL}/api/TodoItems/${this.postDataObj.TodoItemID}`
                    : `${import.meta.env.VITE_API_BASE_URL}/api/TodoItems/`;

                if (requestOptions.method == 'PUT') {
                    fetch(url, requestOptions)
                        .then(response => {
                            this.loadTodos();
                            console.log('Success fetching data:', response);
                        })
                        .catch(error => {
                            console.error('Error fetching data:', error);
                        });
                }
                else {
                    fetch(url, requestOptions)
                        .then(response => response.json())
                        .then(data => {
                            this.initPostData();
                            this.loadTodos();
                            console.log('Success fetching data:', data);
                        })
                        .catch(error => {
                            console.error('Error fetching data:', error);
                        });
                }
            }
        }
    };
</script>

<style>
    @import url('https://fonts.googleapis.com/css2?family=Gochi+Hand&display=swap');
    body {
        font-family: 'Gochi Hand', cursive;
        font-size: 1.2em;
    }
</style>