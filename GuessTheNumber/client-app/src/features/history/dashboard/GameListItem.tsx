import React from 'react';
import { Link } from 'react-router-dom';
import { Button, Icon, Item, Label, Segment } from 'semantic-ui-react';
import { GameResultShort } from '../../../app/models/game';
import {format} from 'date-fns';
import {useStore} from "../../../app/stores/store";

interface Props {
    game: GameResultShort
}

export default function GameListItem({ game }: Props) {

    const {userStore} = useStore();
    return (
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <Item.Image style={{marginBottom: 3}} size='tiny' circular src={'/assets/user.png'} />
                        <Item.Content>
                            <Item.Header as='h3'
                            >
                                {game.id}
                            </Item.Header>
                            <Item.Description>
                                Hosted by {game.host}
                                {/*Hosted by <Link to={`/profiles/${game.host}`}>{game.host?.displayName}</Link>*/}
                            </Item.Description>
                            {game.host === userStore.user?.userName && (
                                <Item.Description>
                                    <Label basic color='orange'>
                                        You are hosted this game
                                    </Label>
                                </Item.Description>
                            )}
                            {game.winner  === userStore.user?.userName && (
                                <Item.Description>
                                    <Label basic color='green'>
                                        You won this game
                                    </Label>
                                </Item.Description>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock' /> {format(game.endTime!, 'dd MMM yyyy h:mm aa')}
                    <Icon name='question circle' /> {game.guessedNumber}
                    <Icon name='users'/> {game.playersCount}
                </span>
            </Segment>
            <Segment clearing>
                <span>Started: {game.startTime}</span>
                <Button 
                    as={Link}
                    to={'/history'}
                    color='teal'
                    floated='right'
                    content='View details'
                />
            </Segment>
        </Segment.Group>
    )
}