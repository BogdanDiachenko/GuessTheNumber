import { observer } from 'mobx-react-lite';
import React from 'react';
import { NavLink } from 'react-router-dom';
import { Container, Menu, Image, Dropdown } from 'semantic-ui-react';
import { useStore } from '../stores/store';

export default observer(function NavBar() {
    const { userStore: { user, logout } } = useStore();
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} exact to='/' header>
                    <img src={'/assets/logo.png'} alt='logo' style={{ marginRight: '10px' }} />
                    Guess The Number
                </Menu.Item>
                <Menu.Item as={NavLink} to='/history' name='History' />
                <Menu.Item as={NavLink} to='/game' name='Game' />
                <Menu.Item position='right'>
                    <Image src={'/assets/user.png'} avatar spaced='right' />
                    <Dropdown pointing='top left' text={user?.email}>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})