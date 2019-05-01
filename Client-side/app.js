const server = "http://localhost:50753"; // Please change me to match the api url if needed.

const yourTurn = 'Your turn!';
const thinking = 'Thinking...';
const playerWin = 'You won!!';
const computerWin = 'You lost! Try again!';
const typeSomething = 'You need to type something to play.';

Vue.component('word-list', {
    props: {
        words: Array
    },
    template: '<li>{{ (words.isPlayer ? "player:" : "machine: ") + words.text }}</li>'
});

Vue.directive('focus', {
    inserted: function (el) {
        el.focus()
    }
});

const app = new Vue({
    el: '#app',
    data: {
        message: '',
        words: [],
        maxLetters: 1,
        status: yourTurn,
        gameOver: false,
        player: 'player',
        machine: 'machine'
    },
    methods: {
        backspace: function () {
            // This function prevent the user to delete more letters that expected.
            if (!this.gameOver && this.message.length >= this.maxLetters) {
                this.message = this.message.substr(0, this.message.length - 1);
            }
        },
        reset: function () {
            this.gameOver = false;
            this.status = yourTurn;
            this.maxLetters = 1;
            this.message = '';
            this.words = [];
        },
        send: function () {
            if (this.gameOver) {
                this.reset(); // if hit sent again, reset the game.
                return;
            }

            if (!this.message || this.message.length < this.maxLetters) {
                this.status = typeSomething;
                return;
            }

            this.words.push({
                text: this.message,
                isPlayer: true
            });

            this.status = thinking;

            this.$http.get(server + '/api/ghost/play', {
                params: {
                    word: this.message
                }
            }).then(
                response => {
                    
                    switch (response.data.status) {
                        case 0:
                            // Continue
                            this.status = yourTurn;
                            this.maxLetters += 2; // Can add one letter more, computers added one already.
                            this.message = response.data.element;
                            this.words.push({
                                text: response.data.element,
                                isPlayer: false
                            });
                            break;
                        case 1:
                            // Computer win
                            this.status = computerWin;
                            this.gameOver = true;
                            break;
                        case 2:
                            // Player win
                            this.status = playerWin;
                            this.gameOver = true;
                            this.words.push({
                                text: response.data.element,
                                isPlayer: false
                            });
                            break;
                    }
                },
                error => {
                    this.status = "Error! " + error.body;
                    this.gameOver = true;
                }
            );

        }
    }
});