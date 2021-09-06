import { ErrorMessage, Form, Formik } from 'formik';
import { observer } from 'mobx-react-lite';
import React from 'react';
import { Button, Header } from 'semantic-ui-react';
import MyTextInput from '../../app/common/form/MyTextInput';
import { useStore } from '../../app/stores/store';
import * as Yup from 'yup';
import ValidationErrors from '../errors/ValidationErrors';

export default observer(function RegisterForm() {
    const {userStore} = useStore();
    return (
        <Formik
            initialValues={{name: '', surname:'', userName: '', email: '', password: '', confirmPassword:'', error: null}}
            onSubmit={(values, {setErrors}) => userStore.register(values).catch(error =>
                setErrors({error}))}
            validationSchema={Yup.object({
                name: Yup.string().required(),
                surname: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required(),
                confirmPassword: Yup.string().required()
            })}
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className='ui form error' onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Sign up to Reactivites' color='teal' textAlign='center' />
                    <MyTextInput label='Name' name='name' placeholder='Name' />
                    <MyTextInput label='Surname' name='surname' placeholder='Surname' />
                    <MyTextInput label ='Username' name='username' placeholder='Username' />
                    <MyTextInput label='Email' name='email' placeholder='Email' />
                    <MyTextInput label='Password' name='password' placeholder='Password' type='password' />
                    <MyTextInput label='Password confirmation' name='confirmPassword' placeholder='Confirm Password' type='password' />
                    <ErrorMessage
                        name='error' render={() =>
                        <ValidationErrors errors={errors.error}/>}
                    />
                    <Button disabled={!isValid || !dirty || isSubmitting}
                            loading={isSubmitting} positive content='Register' type='submit' fluid />
                </Form>
            )}
        </Formik>
    )
})