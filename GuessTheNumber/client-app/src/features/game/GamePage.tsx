import React from 'react'
import {observer} from 'mobx-react-lite'
import { Button, Container, Header, Image, Segment } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { useStore } from '../../app/stores/store'

export default observer(function GamePage(){
    const {gameStore} = useStore()
    return(
      <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header  as='h1' inverted>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
                    Guess The Number
                </Header>
                {/* {gameStore.isLoggedIn ? (
                    <>
                        <Header as='h2' inverted content='There is no started games. Press button bellow to start new game.' />
                        <Button as={Link} to='/history' size='huge' inverted>
                            Start new game.
                        </Button>
                    </> */}

                {/* // ) : (
                //         <>
                //             <Button onClick={() => modalStore.openModal(<LoginForm />)} size='huge' inverted>
                //                 Login!
                //         </Button>
                //             <Button onClick={() => modalStore.openModal(<RegisterForm />)} size='huge' inverted>
                //                 Register!
                //         </Button>
                        </>

                    )} */}
                
                <Button positiv primary onClick={gameStore.getCurrentGame}>GetCurrentGame</Button>
                <Button positiv primary onClick={gameStore.logGame}>LogCurrentGame</Button>

            </Container>
        </Segment>
    )
})