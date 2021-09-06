import { observer } from 'mobx-react-lite';
import React from 'react'
import { Link } from 'react-router-dom';
import { Button, Header, Item, Segment, Image, Label } from 'semantic-ui-react'
import { GameResultFull } from "../../../app/models/game";
import { format } from 'date-fns';
import { useStore } from '../../../app/stores/store';

const gameImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    activity: GameResultFull
}

export default observer(function GameResultFullDetailedHeader({ activity }: Props) {
    const { historyStore: {
    } } = useStore();
    return (
        <Segment.Group>
            <Segment basic attached='top' style={{ padding: '0' }}>
                {/*<Segment style={activityImageTextStyle} basic>*/}
                {/*    <Item.Group>*/}
                {/*        <Item>*/}
                {/*            <Item.Content>*/}
                {/*                <Header*/}
                {/*                    size='huge'*/}
                {/*                    content={activity.title}*/}
                {/*                    style={{ color: 'white' }}*/}
                {/*                />*/}
                {/*                <p>{format(activity.date!, 'dd MMM yyyy')}</p>*/}
                {/*                <p>*/}
                {/*                    Hosted by <strong><Link to={`/profiles/${activity.host?.username}`}>{activity.host?.displayName}</Link></strong>*/}
                {/*                </p>*/}
                {/*            </Item.Content>*/}
                {/*        </Item>*/}
                {/*    </Item.Group>*/}
                {/*</Segment>*/}
            </Segment>
        </Segment.Group>
    )
})