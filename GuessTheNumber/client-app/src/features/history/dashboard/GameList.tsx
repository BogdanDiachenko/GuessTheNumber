import { observer } from 'mobx-react-lite';
import React, { Fragment } from 'react';
import { Header } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';
import GameListItem from './GameListItem';

export default observer(function GameList() {
    const {  historyStore } = useStore();
    const { groupedHistory  } = historyStore;

    return (
        <>
            {groupedHistory.map(([group, games]) => (
                <Fragment key={group}>
                    <Header sub color='teal'>
                        {group}
                    </Header>
                    {games.map(game => (
                        <GameListItem key={game.id} game={game} />
                    ))}
                </Fragment>
            ))}
        </>

    )
})