import * as React from 'react';
// import * as ReactDOM from 'react-dom';
import './index.css';

interface SquareProps {
    value: string,
    onClick: () => void
}

function Square(props: SquareProps): JSX.Element {
    return (
        <button className="square" onClick={props.onClick}>
            {props.value}
        </button>
    );
}

interface BoardProps {
    squares: string[],
    onClick: (i: number) => void,
}

class Board extends React.Component<BoardProps, {}> {
    renderSquare(i: number) {
        return <Square
            value={this.props.squares[i]}
            onClick={() => this.props.onClick(i)}
        />;
    }

    render() {
      console.log("boardhello");
        return (
            <div>
                <div className="board-row">
                    {this.renderSquare(0)}
                    {this.renderSquare(1)}
                    {this.renderSquare(2)}
                </div>
                <div className="board-row">
                    {this.renderSquare(3)}
                    {this.renderSquare(4)}
                    {this.renderSquare(5)}
                </div>
                <div className="board-row">
                    {this.renderSquare(6)}
                    {this.renderSquare(7)}
                    {this.renderSquare(8)}
                </div>
            </div>
        );
    }
}

interface GameState {
    history: { squares: string[] }[],
    stepNumber: number,
    xIsNext: boolean,
}

// interface Props{
// }

class Game extends React.Component<{},GameState> {
    constructor() {
        super({});
        const initsqrs = [] as string[];
        for (let i = 0; i < 9; i++) {
            initsqrs[i] = "";
        }
        this.state = {
            history: [{ squares: initsqrs }],
            stepNumber: 0,
            xIsNext: true,
        };
      // this.handleClick = this.handleClick.bind(this);
      // call: this.handleClick
    }

    handleClick = (i: number) => {
        const history = this.state.history.slice(0, this.state.stepNumber + 1);
        const current = history[history.length - 1];
        const squares = current.squares.slice();
        if (calculateWinner(squares) || squares[i]) {
            return;
        }
        squares[i] = this.state.xIsNext ? "X" : "O";
        this.setState({
            history: history.concat([{ squares: squares }]),
            stepNumber: history.length,
            xIsNext: !this.state.xIsNext,
        });
    }

    jumpTo(step: number) {
        this.setState({
            stepNumber: step,
            xIsNext: (step % 2) === 0,
        })
    }

    render() {
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const winner = calculateWinner(current.squares);

        const moves = history.map((step, move) => {
            const desc = move ? "Move #" + move : "Game Start";
            return (
                <li key={move}>
                    <button onClick={() => this.jumpTo(move)}>{desc}</button>
                </li>
            );
        });

        let status: string;
        if (winner) {
            status = "Winner: " + winner;
        } else {
            if (current.squares.every((s) => s != null)) {
                status = "Draw game";
            } else {
                status = 'Next player: ' + (this.state.xIsNext ? "X" : "O");
            }

        }

        return (
            <div className="game">
                <div className="game-board">
                    <Board
                        squares={current.squares}
                        onClick={this.handleClick}
                    />
                </div>
                <div className="game-info">
                    <div>{status}</div>
                    <ol>{moves}</ol>
                </div>

            </div>
        );
    }
}


// ========================================

// ReactDOM.render(
//     <Game />,
//     document.getElementById('root')
// );


// ========================================

function calculateWinner(squares: string[]): string {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],
    ];
    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return squares[a];
        }
    }
    return "";
}

export default Game;