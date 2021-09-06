import { observer } from 'mobx-react-lite';
import React from 'react'
import { Segment, Grid, Icon } from 'semantic-ui-react'
import { GameResultFull } from "../../../app/models/game";
import {format} from 'date-fns';

interface Props {
    game: GameResultFull
}

export default observer(function GameDetailedInfo({ game }: Props) {
    return (
        <Segment.Group>
            <Segment attached='top'>
            {/*    <Grid>*/}
            {/*        <Grid.Column width={1}>*/}
            {/*            <Icon size='large' color='teal' name='info' />*/}
            {/*        </Grid.Column>*/}
            {/*        <Grid.Column width={15}>*/}
            {/*            <p>{game.}</p>*/}
            {/*        </Grid.Column>*/}
            {/*    </Grid>*/}
            {/*</Segment>*/}
            {/*<Segment attached>*/}
            {/*    <Grid verticalAlign='middle'>*/}
            {/*        <Grid.Column width={1}>*/}
            {/*            <Icon name='calendar' size='large' color='teal' />*/}
            {/*        </Grid.Column>*/}
            {/*        <Grid.Column width={15}>*/}
            {/*            <span>*/}
            {/*                {format(game!, 'dd MMM yyyy h:mm aa')}*/}
            {/*            </span>*/}
            {/*        </Grid.Column>*/}
            {/*    </Grid>*/}
            {/*</Segment>*/}
            {/*<Segment attached>*/}
            {/*    <Grid verticalAlign='middle'>*/}
            {/*        <Grid.Column width={1}>*/}
            {/*            <Icon name='marker' size='large' color='teal' />*/}
            {/*        </Grid.Column>*/}
            {/*        <Grid.Column width={11}>*/}
            {/*            <span>{game.venue}, {game.city}</span>*/}
            {/*        </Grid.Column>*/}
            {/*    </Grid>*/}
            </Segment>
        </Segment.Group>
    )
})