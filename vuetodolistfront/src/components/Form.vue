<template>
    <form @submit.prevent="postData">
        <div class="mb-3 row">
            <label for="title" class="col-md-4 col-form-label">Title</label>
            <div class="col-md-8">
                <input type="text" id="title" class="form-control" v-model="postDataObj.title" required>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="description" class="col-md-4 col-form-label">Description</label>
            <div class="col-md-8">
                <input type="text" id="description" class="form-control" v-model="postDataObj.description" required>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="dueDate" class="col-md-4 col-form-label">Due Date</label>
            <div class="col-md-8">
                <input type="datetime-local" id="dueDate" class="form-control" v-model="postDataObj.dueDate" required>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="category" class="col-md-4 col-form-label">Category</label>
            <div class="col-md-8">
                <select id="category" class="form-select" v-model="postDataObj.categoryId" required>
                    <option v-for="category in categories" :key="category.categoryID" :value="category.categoryID">{{ category.categoryName }}</option>
                </select>
            </div>
        </div>
        <div class="mb-3">
            <div v-if="editMode == true">
                <button type="submit" class="btn btn-success">Edit</button>
            </div>
            <div v-else>
                <button type="submit" class="btn btn-primary">Submit</button>
            </div>
        </div>
    </form>
</template>

<script>
    export default {
        props: {
            postDataObj: Object,
            editMode: Boolean,
            categories: Array,
        },
        methods: {
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
                            this.$emit('load-todo');
                            console.log('Success fetching data:', response);
                        })
                        .catch(error => {
                            console.error('Error fetching data:', error);
                        });
                } else {
                    fetch(url, requestOptions)
                        .then(response => response.json())
                        .then(data => {
                            this.$emit('init-post');
                            this.$emit('load-todo');
                            console.log('Success fetching data:', data);
                        })
                        .catch(error => {
                            console.error('Error fetching data:', error);
                        });
                }
            },
        },
    };
</script>
