const express = require('express');
const { getUsers, getUser, updateUser, addUser, deleteUser } = require('../Controllers/userController');

const router = express.Router();

//GET ALL USERS
router.get('/users', getUsers);

//GET USER BY ID
router.get('/users/:id', getUser);

//UPDATE USER
router.put('/users/:id', updateUser);

//CREATE USER
router.post('/users', addUser);

//DELETE USER
router.delete('/users/:id', deleteUser);

module.exports = {
    routes: router
}