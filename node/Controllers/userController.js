'use strict';

const firebase = require('../db');
const User = require('../Models/User');
const firestore = firebase.firestore();

const getUsers = async (req, res, next) => {
    try {
        const users = await firestore.collection('users');
        const data = await users.get();
        const usersList = [];

        if(data.empty){
            res.status(404).send('No users found!');
        }else{
            data.forEach(doc => {
                const user = new User(
                    doc.id,
                    doc.data().email,
                    doc.data().firstName,
                    doc.data().lastName,
                    doc.data().birthday,
                    doc.data().phoneNumber,
                    doc.data().status,
                )
                usersList.push(user);
            });
            res.send(usersList);
        }
    } catch (error) {
        res.status(400).send(error.message);
    }
}

const getUser = async (req, res, next) => {
    try {
        const id = req.params.id;
        const user = await firestore.collection('users').doc(id);
        const data = await user.get();

        if(!data.exists){
            res.status(404).send(`Could not find User with id: ${id}`);
        }else{
            res.send(data.data());
        }
    } catch (error) {
        res.status(400).send(error.message);
    }
}

const updateUser = async (req, res, next) => {
    try {
        const id = req.params.id;
        const data = req.body;
        const user = await firestore.collection('users').doc(id);
        const userData = await user.get();

        if(!userData.exists){
            res.status(404).send(`Could not find User with id: ${id}`);
        }else{
            await user.update(data);
            res.send('User updated successfully!');
        }
    } catch (error) {
        res.status(400).send(error.message);
    }
}

const addUser = async (req, res, next) => {
    try {
        const data = req.body;
        await firestore.collection('users').doc().set(data);
        res.send('User created successfully!');
    } catch (error) {
        res.status(400).send(error.message);
    }
}

const deleteUser = async (req, res, next) => {
    try {
        const id = req.params.id;
        await firestore.collection('users').doc(id).delete();
        res.send('User deleted successfully!');
    } catch (error) {
        res.status(400).send(error.message);
    }
}



module.exports = {
    getUsers,
    getUser,
    updateUser,
    addUser,
    deleteUser
}