module.exports = {
  root: true,
  parser: '@typescript-eslint/parser',
  parserOptions: {
    tsconfigRootDir: __dirname,
    project: ['./tsconfig.json'],
  },
  plugins: [
    'import',
    '@typescript-eslint',
  ],
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:@typescript-eslint/recommended-requiring-type-checking',
  ],
  rules: {
    "array-bracket-spacing": ["warn", "always"],
    "eqeqeq": ["error", "smart"],
    "no-nested-ternary": "error",
    "object-curly-spacing": ["warn", "always"],
    "object-property-newline": ["warn", { "allowAllPropertiesOnSameLine": true }],
    "prefer-const": "error",
    "@typescript-eslint/quotes": ["warn", "single", { "avoidEscape": true }],
    "@typescript-eslint/semi": ["error", "always"],
    "import/no-default-export": "error",
  },
};